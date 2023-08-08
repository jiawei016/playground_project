# Introduction
Produce voucher triggered from Angular app, data will flow into API then it will save into postgres and publish topic to kafka broker, worker service will subscribe the topic and save to redis, Redeemed voucher api  triggered from Angular app will then get value from redis and display on UI.

# Architecture Diagram
![image](https://github.com/jiawei016/playground_project/assets/12298454/a1ce1971-bfcb-4acf-a9ab-f5e3aaa84285)

# voucher_system
# Install These Environment in order to debug in local
<b>Download NVM</b> from [nvm-windows#readme](https://github.com/coreybutler/nvm-windows#readme) <br />
Open CMD to run these commands
````
nvm install 19.9.0
````
````
nvm use 19.9.0
````
````
npm install -g @angular/cli@16.1.8
````
Open Visual Studio Code and Enter this line of code for not being restricted from execute the source code
````
Set-ExecutionPolicy Unrestricted -Scope CurrentUser
````

# Step 1: Create a network
````
docker network create -d bridge my_voucher_system_infra_network
````

# Step 2: Create a Postgres Volume
````
docker volume create postgres-volume
````

# Step 3: Execute docker-compose
````
docker-compose -f "docker-compose-production.yaml" up -d
````
