# ML Agents Training Setup - Alternative Approach

Due to compatibility issues with the current Python version, here's an alternative setup:

## Option 1: Use Python 3.8-3.10 (Recommended)

1. **Install Python 3.9**: Download from python.org
2. **Create virtual environment**:
   ```
   python3.9 -m venv ml_agents_env
   ml_agents_env\Scripts\activate
   ```
3. **Install ML Agents**:
   ```
   pip install mlagents==0.28.0
   pip install protobuf==3.20.3
   ```
4. **Start training**:
   ```
   mlagents-learn config/flappy_bird_config.yaml --run-id=flappy_bird_training
   ```

## Option 2: Use Unity's Built-in Training

1. **In Unity**: Window → Package Manager
2. **Search for**: "ML Agents"
3. **Install**: ML Agents package
4. **Add components to your bird**:
   - Decision Requester
   - Behavior Parameters (set to "FlappyBird")
5. **Use the FlappyBirdAgent script** (already created)

## Option 3: Manual Training Loop

Your FlappyBirdAgent.cs is ready with:
- ✅ Observations (5 inputs)
- ✅ Actions (jump/no jump)
- ✅ Rewards system
- ✅ Episode management

## Unity Scene Setup Required:

1. **Create GameObjects**:
   - Bird (with Rigidbody2D, CircleCollider2D)
   - Ground (with BoxCollider2D)
   - Pipes (with BoxCollider2D)

2. **Add Components to Bird**:
   - FlappyBirdAgent script
   - Decision Requester
   - Behavior Parameters

3. **Configure Behavior Parameters**:
   - Behavior Name: "FlappyBird"
   - Vector Observation: 5
   - Actions: Discrete, Size 1, Branch 0 Size 2

## Training Results Location:
- Results will be saved in `results/flappy_bird_training/`
- TensorBoard logs for graphs and CSV export
- Trained model (.onnx file) for deployment

## For TensorBoard Visualization:
```
tensorboard --logdir results
```
Then open: http://localhost:6006

The training configuration is optimized for Flappy Bird with:
- PPO algorithm
- 500k max steps
- Reward: +0.1 alive, +5 pass pipe, -10 collision