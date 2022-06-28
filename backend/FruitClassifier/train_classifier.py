from tensorflow.keras.layers import Conv2D, MaxPooling2D, Dense, Flatten
from tensorflow.keras.models import Sequential
from tensorflow.keras.utils import to_categorical
import argparse
import os
from PIL import Image
import numpy as np


def train_model(data_path):
    classes = [x for x in os.listdir(data_path) if not x.startswith(".")]
    X_train, Y_train = [],[]
    X_test, Y_test = [],[]
    train_test_ratio = 0.2
    for cl in classes:
        n_images = len(os.listdir(os.path.join(data_path, cl)))
        n_train = int(n_images * (1-train_test_ratio)) if n_images > 3 else n_images
        for i, filename in enumerate(os.listdir(os.path.join(data_path, cl))):
            im = Image.open(os.path.join(data_path, cl, filename))
            if i < n_train:
                X_train.append(np.asarray(im))
                Y_train.append(classes.index(cl))
            else:
                X_test.append(np.asarray(im))
                Y_test.append(classes.index(cl))
    X_train = np.asarray(X_train)
    Y_train = np.asarray(Y_train)
    X_test = np.asarray(X_test)
    Y_test = np.asarray(Y_test)


    # Normalize the images.
    X_train = (X_train / 255) - 0.5
    X_test = (X_test / 255) - 0.5

    # Reshape the images.
    #X_train = np.expand_dims(X_train, axis=3)
    #X_test = np.expand_dims(X_test, axis=3)

    num_filters = 8
    filter_size = 3
    pool_size = 2

    model = Sequential([
      Conv2D(num_filters, filter_size, input_shape=(100, 100, 3)),
      MaxPooling2D(pool_size=pool_size),
      Flatten(),
      Dense(len(classes), activation='softmax'),
    ])

    model.compile(
        'adam',
        loss='categorical_crossentropy',
        metrics=['accuracy'],
    )

    model.fit(
        X_train,
        to_categorical(Y_train),
        epochs=3,
        validation_data=(X_test, to_categorical(Y_test)),
    )
    model.evaluate(X_test, to_categorical(Y_test))
    model.save("fruit_classifier")

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--data", default="/Users/doriandrost/repos/mini-rem/data")
    args = parser.parse_args()
    train_model(args.data)
