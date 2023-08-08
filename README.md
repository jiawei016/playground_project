# playground_project
playground_project

# System 2
# voucher_system
# Install These Environment
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
