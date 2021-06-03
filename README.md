# MingleAvatar

How to use:

1. Run the code by selecting the "Mingle" solution at top and clicking F5.
2. The Swagger UI will show after the application builds and runs.
3. In the Swagger UI you can:
  1. GET all available avatars
  2. POST (Create) an avatar, by specifing its properties in json provided by the Swagger UI.
  3. GET an existing avatar by his "Id".
  4. DELETE an avatar by his "Id".
3.1 Whenever an avatar is created it is added to a "SavedAvatars.json" file in the root folder of the Mingle solution.
3.2 There can be only one avatar with a specific Id. If you add an avatar with an "Id", which already exists, you will see an error message.
3.3 If you want to delete an avatar, with "Id", which is not existent, you will see an error message.

Known issues:
1. Sometimes when you run all tests, a test which is checking the serialization may fail. Pressing "Run failed tests" runs the test and it passes as it should.

Feedback:
I believe "Id" should be renamed "Username". Usually, an "Id" is generated from the server and the user doesn't have access to such a field, "Id" could be a ever incrementing int or a guid.
In this case, where the user specifies his "Id", I think it is more a "Username" field.

Remarks:
Shoesize is a float as sometime people want to write 45.5 shoesize.
Color is a string and not an enum, as it can be extended later on so that we add other colors.
The project was not done following TDD. The tests were created after the functionality was implemented.  



