# IHC
El propósito del presente trabajo es desarrollar un juego de ajedrez haciendo uso de la realidad aumentada. El juego es para dispositivos móviles en plataforma Android y el objetivo es tener un tablero virtual que puede ser desplegado en cualquier lugar sin la necesidad de cargar un tablero físico. La idea principal del proyecto es que tanto el tablero de ajedrez como las piezas sean proyectadas sobre una superficie detectada con la cámara del celular. Una vez detectada la superficie, el tablero será posicionado y visto mediante la pantalla del celular. Para la manipulación de las piezas se utilizará la voz de forma que, mediante comandos basados en la nomenclatura estándar de filas y columnas del tablero, se puedan realizar los movimientos propios del ajedrez. Además de esta característica, se realizará la manipulación de las piezas de manera que puedan ser posicionadas en la casilla de destino usando el touch del celular.
# Herramientas usadas durante el desarrollo de la aplicación
* La plataforma de desarrollo base usada es Unity en su versión 2019.4.30f1
* El kit de desarrollo para realidad aumentada es ARCore en su versión 1.14.0
* Los paquetes adicionales usados son:
*   XR Legacy Input Helpers 2.1.8
*   Multiplayer HLAPI 1.0.8
* Módulos incluidos
*   Android Build Support
*   Android SDK y NDK Tools
*   OpenJDK