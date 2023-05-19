# Ground

## ¿Cómo usarlo?
- Instalar Unity en la versión 2021.3.22f1
- Instalar [Git](https://git-scm.com/downloads)
- Clonar este repositorio con el comando:  git clone 
    ```
    https://github.com/CristhianAC/Ground.git
    ```
- Abrir el proyecto en Unity 
- Build and Run el proyecto para probar el juego

## Paquetes necesarios en Unity
- Multiplayer Samples Utilities(https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop.git?path=/Packages/com.unity.multiplayer.samples.coop#main)
- Multiplayer Tools
- Netcode for GameObjects

## INFORMACIÓN SOBRE EL PROYECTO

## Requisitos para la conexión entre Host y Client
- Se crea un NetworkManager que tenga todas las configuraciones necesarias para el proyecto.
- Se usa Unity Transport(UTP) como la capa de transporte que Netcode usa para la comunicación entre el Host y el Client.
- Se crea un NetworkPrefab que contenga los componentes necesarios para sincronizar los GameObjects entre el Host y el Client.

## Uso de Sockets
- Se crea un socket en el Host y en el Cliente que especifique la familia de direcciones, el tipo de socket y el tipo de protocolo que se va a usar.
- Se vincula el socket del Host a una dirección IP y un puerto.
- Se conecta el socket del Cliente al socket del Host usando un IPEndPoint que contenga la dirección IP y el puerto del Host.

## Creación de Escenarios
- Se crea un escenario con Tilemap 
- Se crea Tileset para contener los tiles que se usan en el escenario.
- Se crean 2 Tile Palette para seleccionar y pintar los tiles del Tileset en el Tilemap.
- El asset usado para crear el mapa proviene del siguiente link: https://v3x3d.itch.io/retro-lines

## Leaderboard 
- Se la clase LeaderboardManager para acceder y modificar las tablas de clasificación.
- El asset fue obtenido del siguiente link: https://assetstore.unity.com/packages/tools/utilities/leaderboard-manager-cg-177291

## Materiales de apoyo
- Dentro de este repositorio podrá encontrar una imagen del diagrama de clases en formato .png.
- Ademas de la imagen encontrara el codigo de ese diagrama en formato .puml
