#include <Adafruit_CircuitPlayground.h>

void setup() {
  CircuitPlayground.begin();
  Serial.begin(9600);
}

void loop() {
  // Get accelerometer values for aiming
  float x = CircuitPlayground.motionX();
  float y = CircuitPlayground.motionY();

  // Send motion data over Serial
  Serial.print("X:");
  Serial.print(x);
  Serial.print(",Y:");
  Serial.println(y);

  // Button press to shoot
  if (CircuitPlayground.leftButton()) {
    Serial.println("SHOOT");
    for (int i = 0; i < 10; i++) { // Circuit Playground has 10 LEDs
        CircuitPlayground.setPixelColor(i, 255, 255, 0); // Red color (R=255, G=0, B=0)
      }
    CircuitPlayground.clearPixels();
  }

  delay(50); // Reduce data overload
}
