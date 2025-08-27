# About

This project was created to **automate scheduled message distribution in Telegram**.  
With it, you can:  
- Write a custom message 
- Attach an image
- Set a specific time interval
- Choose target channels

The application will then handle the delivery process automatically.

---
# Installation

To run the project, you will need **.NET 9**.  
Download it here: [Download .NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

Clone the repository:
```bash
git clone https://github.com/Tenshi-AL/TechnoShop.git
```
Build the project:
```bash
dotnet publish
```
Run the app either via the generated shortcut, or with the command:
```bash
dotnet UI.dll --urls "http://localhost:8080"
```

---
# Usage
## Original configuration
Go to the root of the application and in the appsettings.json file, find the TelegramConfiguration section; it looks approximately like this:
```json
"TelegramConfiguration":{
    "ApiId": 1000000000000000,
    "ApiHash": "000000000000000000000",
    "PhoneNumber": "+00000000000000000"
  },
```
Replace this data with your own.
