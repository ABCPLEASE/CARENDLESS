# Lecture: Understanding `AdvancePlayerController.cs`

## Step 1: Introduction

- **Objective**: Understand the purpose and functionality of the `AdvancePlayerController.cs` script.
- **Context**: This script is used to control the player's car in a Unity project.

## Step 2: Variables Declaration

```csharp
public float maxSpeed = 20.0f;
public float acceleration = 10.0f;
public float deceleration = 15.0f;
public float turnSpeed = 180.0f;
public float brakingForce = 20.0f;

[Header("Current State")]
private float currentSpeed = 0.0f;
private float horizontalInput = 0;
private float forwardInput = 0;
private bool isBraking = false;
```

- **Explanation**:
  - `maxSpeed`: The maximum speed the car can reach.
  - `acceleration`: The rate at which the car speeds up.
  - `deceleration`: The rate at which the car slows down when no input is given.
  - `turnSpeed`: The speed at which the car turns.
  - `brakingForce`: The force applied when braking.
  - `currentSpeed`: The current speed of the car.
  - `horizontalInput` and `forwardInput`: Store the horizontal and forward input values.
  - `isBraking`: A boolean to check if the player is braking.

## Step 3: Update Method

```csharp
void Update()
{
    // Get input values
    InputAction moveAction = InputSystem.actions.FindAction("Move");
    Vector2 input = moveAction.ReadValue<Vector2>();
    horizontalInput = input.x;
    forwardInput = input.y;
```

- **Explanation**:
  - `Update()`: Called once per frame.
  - `InputAction moveAction`: Retrieves the "Move" action from the input system.
  - `Vector2 input`: Reads the input values.
  - `horizontalInput` and `forwardInput`: Store the horizontal and forward input values.

## Step 4: Handling Braking

```csharp
isBraking = Input.GetKey(KeyCode.Space);
```

- **Explanation**:
  - Checks if the spacebar is pressed to determine if the player is braking.

## Step 5: Acceleration and Deceleration

```csharp
// Apply acceleration/deceleration
if (forwardInput != 0)
{
    currentSpeed += forwardInput * acceleration * Time.deltaTime;
    currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
}
else
{
    float decelAmount = deceleration * Time.deltaTime;
    if (Mathf.Abs(currentSpeed) <= decelAmount)
        currentSpeed = 0;
    else
        currentSpeed -= Mathf.Sign(currentSpeed) * decelAmount;
}
```

- **Explanation**:
  - If `forwardInput` is not zero, the car accelerates.
  - `currentSpeed` is adjusted based on `forwardInput`, `acceleration`, and `Time.deltaTime`.
  - `Mathf.Clamp` ensures `currentSpeed` does not exceed `maxSpeed` in either direction.
  - If `forwardInput` is zero, the car decelerates naturally.
  - `decelAmount` is calculated based on `deceleration` and `Time.deltaTime`.
  - If `currentSpeed` is less than or equal to `decelAmount`, it is set to zero.
  - Otherwise, `currentSpeed` is reduced by `decelAmount` in the direction of the current speed.

## Step 6: Applying Braking

```csharp
if (isBraking)
{
    float brakeAmount = brakingForce * Time.deltaTime;
    currentSpeed = Mathf.Max(0, currentSpeed - brakeAmount);
}
```

- **Explanation**:
  - If `isBraking` is true, apply the braking force to reduce the car's speed.
  - `brakeAmount` is calculated based on `brakingForce` and `Time.deltaTime`.
  - `currentSpeed` is reduced by `brakeAmount`, but not below zero.

## Step 7: Turning the Car

```csharp
if (horizontalInput != 0)
{
    float turn = horizontalInput * turnSpeed * Time.deltaTime;
    transform.Rotate(0, turn, 0);
}
```

- **Explanation**:
  - If `horizontalInput` is not zero, the car turns.
  - `turn` is calculated based on `horizontalInput`, `turnSpeed`, and `Time.deltaTime`.
  - `transform.Rotate` rotates the car around the Y-axis.

## Step 8: Summary

- **Recap**:
  - The script handles player input for moving, braking, and turning.
  - It adjusts the car's speed based on input and braking.
  - Uses Unity's `InputSystem` for handling input actions.
