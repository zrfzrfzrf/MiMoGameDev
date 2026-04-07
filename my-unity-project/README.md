# My Unity Project

This project is a Unity game that features two player characters, MiPlayer and MoPlayer, and includes camera functionality to follow the players.

## Project Structure

- **Assets**
  - **Scenes**
    - `MainScene.unity`: Contains the Unity scene setup with player objects.
  - **Scripts**
    - `PlayerMovement.cs`: Handles player movement logic.
    - `CameraFollow.cs`: Manages camera movement to follow the player.
  - **Prefabs**
    - `MiPlayer.prefab`: Prefab for the MiPlayer character with Rigidbody2D and Collider2D components.
    - `MoPlayer.prefab`: Prefab for the MoPlayer character with Rigidbody2D and Collider2D components.

## Setup Instructions

1. Open the project in Unity.
2. Navigate to the `Assets/Scenes` folder and open `MainScene.unity`.
3. Ensure that both MiPlayer and MoPlayer prefabs are present in the scene.
4. Attach the `PlayerMovement` script to both player objects.
5. Set the `Role` property in the `PlayerMovement` script for each player.
6. Attach the `CameraFollow` script to the Main Camera and set it to follow one of the player characters for testing.

## Components

- **PlayerMovement**: This script allows players to move based on input and updates their position accordingly.
- **CameraFollow**: This script updates the camera's position to follow the specified player, ensuring a smooth gameplay experience.

## Notes

Make sure to configure the Rigidbody2D and Collider2D components properly for both player prefabs to ensure correct physics interactions.