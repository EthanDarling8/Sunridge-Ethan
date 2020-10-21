# Sunridge

## TODO List:

### Entire site should be usable on a phone with no functionality lost. Everything it has now should be there, but fixed.

### Board members page:
	- Make more dynamic.
  
### Classifieds page:
	- Fix broken links.
	- Format Prices.
	- Phone and email should be automatically populated.
	- Description should use tiny MCE

### Business and services page:
	- Allow uploading of more than just images.
	- Improve page layout.
	- Allow sorting.

### Documents:
	- They are static at the moment, make them dynamic.
	- Make an admin interface for adding/removing documents.
	- Could add a search and improve presentation
	- By-laws is just a word documents. Make it consistent as a .pdf or a page.

### Events Calendar:
	- Uses third party javascript right now.
	- Read from database table.
	- Works well right now.

### Fire Info:
	- Is static at the moment, make it dynamic.
	- Make searchable.
	- Add Pagination.
	- Make database driven.

### News:
	- Should automatically add a new year when a new article is added to a year.
	- Lost and found is static, make it dynamic
	- Make accessible by owners

### Photo Gallery:
	- Logged in owner will only see their own photos in the dashboard.
	- Admin should see all.

### Banner Items:
	- Improve UI in general.
	- Set expiration date.
	- Use tinyMCE for better presentation.

### Dashboard = Admin's Page:
	- Owners:
		- Currently accounts are added manually.
		- Setup identity framework:
			- Let users self register, but have admin approve the users.
			- Admin has ability to give and take away admin abilities for any user.	
			- Board members are admins.
			- Need a password reset page for users. Can be done through Identity Framework.
			- Do NOT delete users, just lock their account so history is maintained.
			- When owners are logged in, they can see their own information page for setting address and emergency contact information.
			- Same page admin would see for that user when editing owners.

	- Lots:
		- A lot can have many owners.
		- Owners are added by the admin. This is set under the Lots page.
		- Allow admin to add more and more owners dynamically (no owner cap).
		- Fix transferring property interface (It is really bad).
		- Address of lot is seperate from mailing address.
		- People do not live up there year round, mail cannot be delivered.
		- An owner can have many lots.

	- Files:
		- Need to be able to add files to a lot.
		- Need dates and more information on UI.

	- Board Minutes:
		- Need a place to store board minutes files in particular.

	- Keys: 
		- 350 lots * 8 keys.
		- Physical keys to access gated area given to owners every 4 years.
		- First 8 keys are free per lot.
		- Assigning a key to someone should be done by serial number.
		- Keep track of returned keys.
		- Each returned key is given a free updated key.
		- Need a quick way for returning keys and giving out new.
		- Keys while keeping track of serial numbers.
	- Forms:
		- Need a place where users can submit a new form.
		- Should be reviewable by board.
		- Ability to  respond to owners.

	- Work in kind:
		- Filled out by owners when they do work around the area clearing brush or trees, etc.

	- Complaints:

	- Common Area Assets:
		- Track HOA owned stuff.
		- Maintenance records for it.
