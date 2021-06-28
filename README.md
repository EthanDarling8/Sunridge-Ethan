# Sunridge

## Sensitive data such as database context and personal information has been removed in order to make this repo public for resume use. As a result, there are only a couple commits to change the readme and remove API keys and it will not run in ISS Express. Our repo originally had around 300 commits by the end of the semester.

### This website was built using C# .NET using razor pages and a repository pattern. The group that worked on this site consisted of 6 people. The site itself is meant to replace a personal website that our Professor was using for a community of cabins in his local area.

## View my test site here: https://capstoneethansunridge.azurewebsites.net

## Version 2.0 (Fall 2020) TODO List:

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


## TODO / Known Bugs for Future Groups:
### General:
- [ ] Remove unnecessary using statements
- [ ] Dropdown menus where the main button is also a link dont work on mobile. The main link is just loaded with no opportunity to tap other links. (IE lost and found)
- [ ] Move "create new" button on photo and document admin pages to the left side to match other UIs. Do more general UI matching.
- [ ] Title data on admin pages
- [ ] prevent locking the super admin account
- [ ] Several of the admin areas have no create option
- [ ] Make everything async?
- [ ] Add try/catch to many areas for added stability.
- [ ] Almost all our nav buttons are clipped/squished or otherwise weird on mobile. Should put all the navigation and search into a collapsible menu.
- [ ] Clean up **** ToDo **** comments throughout the solution after addressing their issues.
- [ ] Make a favicon for the site.
- [ ] Admins could reset user passwords?
- [ ] Layout, HomeLayout, AdminLayout, and Classifieds Layout should all be condensed. All the scripts and navbar partials should be in layout, HomeLayout deleted, classifieds layout deleted, AdminLayout should just contain the admin navbar, and therefore could be a partial that is loaded inside layout when the user is an admin.
- [ ] Registering a new user currently logs you in as that user, need to disable this.


### Board Member:
- [ ] Create Position Table
- [ ] Placeholders for unfilled Board positions
- [ ] Tie Position table to Board Members and Owners
- [ ] When deleting a Board Member, the position should stay on the Board Member page but say that it is unfilled.

### Documents:
- [ ] Add try / catch to upset pages OnPost methods + anywhere else that still needs one (controllers are done)
- [ ] Initialize DisplayOrder to the next integer (get highest display order from database, +1). Or display what the current highest display order is so the admin can make a better choice.
- [ ] Better Search algorithm (currently on does "contains").
- [ ] Make variable names and URL arguments more consistent.
- [ ] Change sections so they can have files and/or text within them?
- [ ] Display file type icon next to file download links
- [ ] Disable item (file, section, text, category) so it no longer displays on Documents page, but isn't deleted
- [ ] Ensure name of category is unique. Could make name the key?
- [ ] Display previews of maps. How to preview .pdf?
- [ ] Display category NAME on sectiontext upsert / index instead of the ID
- [ ] Add delete buttons for when logged in as an admin to documents page (so admins don't have to go to the dashboard -> datatable to delete sections, files, sectiontext.
- [ ] Modification history
- [ ] File size limits
- [ ] Clicking a link from search results clears the search. Should probably preserve the search.
- [ ] Back buttons on upsert pages should return to whatever page the user came from. OnPost redirects should have similar behavior.

### Events Calendar:
- [ ] Images don't display well inside the event box. They are HUGE! Need to scale down image display.
- [ ] Start and End time input on upsert need to be made to hh:mm only. Currently has hh:mm:ss:ms
- [ ] Don't display a start time on AllDay=true events
- [ ] Multi-day events only show on 1 day

### Forms:
- [ ] Change into something dynamic like Google Forms

### Key History:
- [ ] Allow the mass creation of keys 
- [ ] Seperate key creation and assignment
- [ ] Put in a missing key field

### Lot:
- [ ] Implement Lot_OwnerFiles.
- [ ] Adjust DataTable UI for better scaling.

### Photo Gallery:
- [ ] Add try / catch to upset pages OnPost methods + anywhere else that still needs one (controllers are done)
- [ ] Be able to select the thumbnail for an album
- [ ] Date on album
- [ ] Date on photo
- [ ] The selected category logic could be greatly simplified by using just "int SelectedCategoryId" rather than the entire category object. Or a cookie.
- [ ] Ensure name of category is unique? Could make name the key? This would require a good bit of logic rework because category selection is preserved in URL arguments.
- [ ] Add logic to photo/upsert.cs to preserve aspect ratio when making photo thumbnails. Currently just makes them all 200x200
- [ ] Could change album thumb logic to get a thumbnail from a photo rather than having a link stored.
- [ ] Admin should be able to disable an album or photo without deleting.
- [ ] Search: gallery does not have any search right now
- [ ] Albums datatable for admins
- [ ] Photos datatable for admins
- [ ] Perhaps assign category to photos rather than album. Filter albums by other metadata.
- [ ] Prevent empty photo albums from being viewed except by album owner or admin when their Id is manually entered into the URL.
- [ ] Improve header by displaying user selections IE: My Albums, "SelectedAlbum", "SelectedCategory"...
- [ ] Back button on Album Edit page when clicked from the Album card should return the user to whichever view they accessed it from. Right now it returns to the album details page 	regardless.
- [ ] Modification history
- [ ] Ensure uploaded file is actually a photo
- [ ] File size limits
- [ ] Preserve myAlbums value when deleting an album.
- [ ] MyAlbums and other values might be better preserved as cookies rather than in the URL. Although, it is better to have selected album in the URL for bookmarking / sharing a link to that album. Cannot link to cookies.
- [ ] Deleting a photo or album refreshes the entire page, clearing all selections. Need to preserve user selections.
