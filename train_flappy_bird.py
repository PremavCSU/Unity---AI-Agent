#!/usr/bin/env python3

import subprocess
import sys
import os

def main():
    # Set environment variables to avoid compatibility issues
    os.environ['PYTHONPATH'] = os.getcwd()
    
    # Training command
    cmd = [
        sys.executable, "-m", "mlagents.trainers.learn",
        "--config=config/flappy_bird_config.yaml",
        "--run-id=flappy_bird_training",
        "--no-graphics"
    ]
    
    print("Starting ML Agents training...")
    print("Command:", " ".join(cmd))
    print("\nPress Ctrl+C to stop training")
    print("=" * 50)
    
    try:
        subprocess.run(cmd, check=True)
    except KeyboardInterrupt:
        print("\nTraining interrupted by user")
    except subprocess.CalledProcessError as e:
        print(f"\nTraining failed with error: {e}")
        return 1
    
    return 0

if __name__ == "__main__":
    sys.exit(main())