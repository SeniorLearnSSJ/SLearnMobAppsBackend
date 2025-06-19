#!/bin/bash

echo "Deleting Data in SeniorLearnDb..."
mongosh "mongodb://localhost:27021/SLearnMobApp_db" --file DataSeeding/DeleteSeedData.js
echo "Data Deleted"