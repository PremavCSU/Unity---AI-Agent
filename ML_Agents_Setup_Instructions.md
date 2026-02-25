# Flappy Bird ML Agents Setup Instructions

## Prerequisites
1. Install Python 3.8 or higher
2. Install ML Agents Python package: `pip install mlagents`

## Unity Setup

### 1. Scene Setup
1. Create a new scene or use the existing SampleScene
2. Create the following GameObjects:
   - **Bird**: A GameObject with SpriteRenderer, Rigidbody2D, and CircleCollider2D
   - **Ground**: A GameObject representing the ground/floor
   - **Pipes**: GameObjects with SpriteRenderer and BoxCollider2D for obstacles
   - **GameManager**: Empty GameObject with the GameManager script

### 2. Component Setup
1. Add the `FlappyBirdAgent` script to your Bird GameObject
2. Add the `GameManager` script to an empty GameObject
3. In the FlappyBirdAgent component:
   - Assign the Bird transform to the `bird` field
   - Assign pipe transforms to the `pipes` array
   - Assign ground transform to the `ground` field

### 3. ML Agents Components
1. Add a `Decision Requester` component to the Bird GameObject
2. Add a `Behavior Parameters` component to the Bird GameObject:
   - Set Behavior Name to "FlappyBird"
   - Set Vector Observation Space Size to 5
   - Set Actions: Discrete Branches Size to 1, Branch 0 Size to 2
   - Set Behavior Type to "Default" for training

## Training Setup

### 1. Training Configuration
The training configuration is already created in `config/flappy_bird_config.yaml`

### 2. Start Training
1. Open terminal/command prompt
2. Navigate to your project directory
3. Run: `mlagents-learn config/flappy_bird_config.yaml --run-id=flappy_bird_training`
4. Press Play in Unity when prompted

### 3. Monitor Training
1. Open another terminal
2. Run: `tensorboard --logdir results`
3. Open browser to `http://localhost:6006`

## Reward System
- **+0.1**: Staying alive each frame
- **+5.0**: Successfully passing through a pipe
- **-10.0**: Collision with pipe or ground (episode ends)

## Observations (5 total)
1. Bird's Y position
2. Bird's Y velocity
3. Horizontal distance to closest pipe
4. Vertical distance to closest pipe
5. Distance to ground

## Actions
- **Discrete Action 0**: Jump (0 = no action, 1 = jump)

## Training Tips
1. Start with shorter training sessions (100k steps)
2. Monitor the reward graph in TensorBoard
3. Adjust hyperparameters if needed
4. The agent should learn to time jumps to avoid obstacles

## Troubleshooting
1. Ensure all GameObjects have proper colliders
2. Check that the Behavior Name matches in both Unity and config file
3. Verify Python ML Agents package is installed correctly
4. Make sure the scene is set up before starting training