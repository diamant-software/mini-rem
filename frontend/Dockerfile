# In dieser Vorlage für eigene Projekte anpassen:
# - "beispiel-app" durch den Namen der eigenen App ersetzen
# - Angular- und Node-Versionen anpassen, je nachdem was eure App benötigt


FROM harbor.diamant-software.de/library/node:14.16.0-diamant-1.0.0 AS base

WORKDIR /src

# Node Pakete installieren
COPY .npmrc .
RUN npm install -g @angular/cli@13.0.0 --globalconfig =./.npmrc
COPY package.json .
COPY package-lock.json .
RUN npm ci

# App übersetzen
COPY src src/
COPY *.json ./
COPY karma.conf.js src/
COPY .browserslistrc ./
# Werden weitere Dateien für die Übersetzung eurer App benötigt? Dann hier kopieren

FROM base as build
RUN ng build --configuration production 

# Dateien brotli und gzip komprimieren, damit die Komprimierung nicht erst
# zur Laufzeit erfolgen muss
# bug in npm 7.x RUN npx precompress@6.3.1 /src/dist/beispiel-app
RUN npm install -g precompress@6.3.1
RUN precompress /src/dist


FROM nginx:latest AS run
EXPOSE 80
COPY --from=build /src/dist/mini-rem-frontend /usr/share/nginx/html
RUN ls -la /usr/share/nginx/html
