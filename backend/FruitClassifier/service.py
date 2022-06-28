from fastapi import FastAPI, Form
from tensorflow.keras.models import load_model
import uvicorn
import numpy as np
from PIL import Image
import os
import shutil
from train_classifier import train_model

data_path = "data"

app = FastAPI()

@app.post("/predict")
def predict(image = Form(), id: str = Form()):
    original_image = Image.open(image.file)
    image = np.asarray(original_image)
    image = (image / 255) - 0.5
    model = load_model("fruit_classifier")
    probas = model.predict(np.asarray([image]))
    classes = [x for x in os.listdir(data_path) if not x.startswith(".")]

    confidence = (np.max(probas) / np.sum(probas))*100
    prediction = classes[np.argmax(probas)]
    if not os.path.exists("new_images"):
        os.makedirs("new_images")
    original_image.save(os.path.join("new_images", id + ".png"))
    # add score to output
    return {"label": prediction, "confidence": confidence}

@app.post("/user_correction")
def correct(id: str = Form(), label: str = Form()):
    if not os.path.exists(os.path.join(data_path, label)):
        os.mkdir(os.path.join(data_path, label))
    shutil.move(os.path.join("new_images", id+".png"), os.path.join(data_path, label, id+".png"))

@app.on_event("startup")
@app.post("/train")
def train():
    train_model(data_path)

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=6003)