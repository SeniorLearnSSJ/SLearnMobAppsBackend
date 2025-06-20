const DbConnect = require("appsettings");

db = connect("mongodb://localhost:27017/SLearnMobApp_db");

const collections = [
  "User",
  "MemberBulletin",
  "OfficialBulletin",
  "UserSetting",
];
collections.forEach((collection) => {
  db.getCollection(collection).drop();
});
//delete in mongodbshell in js
