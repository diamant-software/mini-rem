FROM python:3.9 as main

WORKDIR /app
COPY . .
#RUN curl google.com
RUN apt-get update
#RUN python3 -m pip install --upgrade https://storage.googleapis.com/tensorflow/linux/cpu/tensorflow-0.10.0rc0-cp35-cp35m-linux_x86_64.whl
#RUN apt-get update & apt-get install python-h5py
RUN pip install -r requirements.txt


CMD ["uvicorn", "service:app", "--host", "0.0.0.0", "--port", "6003"]