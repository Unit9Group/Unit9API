# Unit9API
This is the API project for Unit 9 in Advanced C#, 2023.

# Database Setup
There is a Word document (MongoDBDoc.docx) that will contain the instructions to create and seed the MongoDB database. 

# Testing
The app is configured to run on `localhost:7272`. You can make calls in Postman to test the API.
NOTE: You may need to disable SSL verification for this to work. 

**GET**
- `https://localhost:7272/api/Car`
- `https://localhost:7272/api/Motorcycle`
- `https://localhost:7272/api/Truck`
**POST**
- Use a random 24 digit hex code generator for the `Id` field (this one is great)[https://numbergenerator.org/random-24-digit-hex-codes-generator]
- Set the method from `GET` to `POST`
- Use this request body:
```
{
  "Id": "string",
  "Name": "string",
  "Make": "string",
  "Year": 0,
  "Color": "string",
  "Type": "string"
}
```
**PUT**
- Reference objects by `Id` and model (e.g. Car) 
- This will require you to probably fetch the `Id` from the DB first.
- `https://localhost:7272/api/Car/63eec963cae35507607dede1'
- You can use the request body above to make changes to existing fields. Any fields left out will be written as `null` in the DB.
**DELETE**
- Same as `PUT`, but you use `DELETE` method instead.

<<<<<<< Back-End
=======

# Wireframe
<b>Desired page layout</b>

![wireframe](https://user-images.githubusercontent.com/26259906/217272963-01161471-5b09-4698-afbd-416d6212cb84.png)

<b>WIP wireframe (what we will use for testing)</b>

![wireframe2](https://user-images.githubusercontent.com/26259906/217274774-daa58bf6-fddb-4e09-9a0f-de545f787c7e.PNG)
>>>>>>> main
