#!/bin/bash
kubectl create secret generic spaceplanets-mongodb-connectionstring --from-literal="string=mongodb://mongo:27017/SpacePlanets"

#!/bin/bash
kubectl create secret generic spaceplanets-appsettings --from-file=./spaceplanets-appsettings.json
