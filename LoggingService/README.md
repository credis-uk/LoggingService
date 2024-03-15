# Logging Service and Unit Tests

This project includes a logging service and a suite of unit tests to verify its functionality. The logging service supports different log levels and writes logs to a specified folder.

## Prerequisites

Before running the application or the unit tests, you need to install and run Mosquitto, an open-source message broker that uses the MQTT protocol.

### Installing Mosquitto

You can download Mosquitto from the [official website](https://mosquitto.org/download/). Follow the instructions provided for your specific operating system.

### Running Mosquitto

Please note that the unit tests assume Mosquitto is installed in the default location (`C:\Program Files\mosquitto\mosquitto`).

## Running the Application

After installing and running Mosquitto, you can run the application as you normally would.

## Running the Unit Tests

The unit tests are located in the `UnitTests` project. These tests verify the functionality of the logging service, including different log levels and logging thresholds.

To run the unit tests, you can use the integrated test runner in Visual Studio. Right-click on the `UnitTests` project in the Solution Explorer and select Run Unit Tests.

Please note that the unit tests will clear the log folder before each test, so any existing logs will be deleted. If you want to keep your logs, make sure to back them up before running the tests.
