# Sunridge

## TODO List:

### Entire site should be usable on a phone with no functionality lost. Everything it has now should be there, but fixed.

### Board members page: 
- [x] Make more dynamic.
- [x] Database driven.
  
### Classifieds page:
- [x] Fix broken links.
- [x] Format Prices.
- [x] Phone and email should be automatically populated.
- [x] Description should use tiny MCE.

### Business and services page:
- [x] All Under classifieds ads now.
- [x] Allow uploading of more than just images.
- [x] Improve page layout.
- [x] Allow sorting.

### Documents:
- [x] They are static at the moment, make them dynamic.
- [x] Make an admin interface for adding/removing documents.
- [x] Could add a search and improve presentation
- [x] By-laws is just a word documents. Make it consistent as a .pdf or a page.

### Events Calendar:
- [x] Uses third party javascript right now.
- [x] Read from database table.
- [x] Works well right now.

### Fire Info:
- [x] Is static at the moment, make it dynamic.
- [x] Make searchable.
- [x] Add Pagination.
- [x] Make database driven.

### News:
- [ ] Should automatically add a new year when a new article is added to a year.
- [x] Lost and found is static, make it dynamic
- [x] Make accessible by owners

### Photo Gallery:
- [x] Logged in owner will only see their own photos in the dashboard.
- [x] Admin should see all.

### Banner Items:
- [x] Improve UI in general.
- [x] Set expiration date.
- [x] Use tinyMCE for better presentation.

### Dashboard = Admin's Page:
-  Owners:
	- [x] Currently accounts are added manually.
	- Setup identity framework:
		- [x]  Let users self register, but have admin approve the users.
		- [x]  Admin has ability to give and take away admin abilities for any user.	
		- [x]  Board members are admins.
		- [x]  Need a password reset page for users. Can be done through Identity Framework.
		- [x] Do NOT delete users, just lock their account so history is maintained.
		- [x]  When owners are logged in, they can see their own information page for setting address and emergency contact information.
		- [x] Same page admin would see for that user when editing owners.

	- Lots:
	    - [x]  A lot can have many owners.
		- [x] Owners are added by the admin. This is set under the Lots page.
		- [x] Allow admin to add more and more owners dynamically (no owner cap).
		- [x] Fix transferring property interface (It is really bad).
		- [x] Address of lot is separate from mailing address.
		- [x] People do not live up there year round, mail cannot be delivered.
		- [x] An owners can have many lots.

	- Files:
		- [x] Need to be able to add files to a lot.
		- [x] Need dates and more information on UI.

	- Board Minutes:
		- [x] Need a place to store board minutes files in particular.

	- Keys: 
		- [x] 350 lots * 8 keys.
		- [x] Physical keys to access gated area given to owners every 4 years.
		- [x] First 8 keys are free per lot.
		- [x] Assigning a key to someone should be done by serial number.
		- [x] Keep track of returned keys.
		- [x] Each returned key is given a free updated key.
		- [x] Need a quick way for returning keys and giving out new.
		- [x] Keys while keeping track of serial numbers.
	- Forms:
		- [x] Need a place where users can submit a new form.
		- [x] Should be reviewable by board.
		- [x] Ability to  respond to owners.

	- Work in kind:
		- [x] Filled out by owners when they do work around the area clearing brush or trees, etc.

	- [x] Complaints:

	- Common Area Assets:
		- [x] Track HOA owned stuff.
		- [x]  Maintenance records for it.

### TODO for Future Groups:
- [ ] Implement Lot_OwnerFiles.
- [ ] Adjust DataTable UI for better scaling.
