# MedicalService

MedicalService is a Medical Tracker web-application made using ASP.NET Core MVC. It gives doctors an opportunity to make personal schedule of taking medicines and procedures for their patients. MedicalService allows doctors to specify various parameters when adding a medicine/procedure. After adding all medicine can be deleted, changed or viewed. 

# Description

The application has three user roles - patient, doctor, administrator.

Depending on the role of the user and the passage of authorization, the user sees the following pages:

__1. Unauthorized user__
    
        - Login/registration pages
        
    
__2. Authorized user without the role of "patient"__

    - Data entry page for user registration as a "patient" (Creating Patient Profile)
    
 
__3. Authorized user in the role of "patient"__

    - Main page of the "patient" role with a choice: go to the patient profile or the patient calendar
    - Patient profile with the ability to edit profile data
    - Patient Profile Data Edit Pages
    - Patient calendar with displayed medicines and the ability to view each of them (modal windows)


__4. Authorized user in the role of "doctor"__

    - Main page of the "doctor" role with a choice: go to the doctor's profile, to the main page of the "patient" role or to the list of his patients (assigned to this doctor)
    - Doctor profile with the ability to edit profile data
    - Doctor Profile Data Edit Pages
    - List of doctor's patients with the ability to go: to the profile of each of them or to the calendar of each of them
    - The patient's calendar (for each patient of the list) is available for adding medicines to it (modal windows)
    - Main page of the "patient" role with a choice: go to the patient profile or the patient calendar
    - Patient profile with the ability to edit profile data
    - Patient Profile Data Edit Pages
    - Patient calendar with displayed medicines and the ability to view each of them (modal windows)
    
    
__5. Authorized user in the role of "admin"__

    - Has access with the ability to edit any data on all pages available to users in the role of "doctor" or "patient"
    - Main page of the "admin" role with a choice: go to the list of system users, to the list of patients or to the list of doctors
    - List of system users
    - Page for creating a new system user
    - System user editing page
    - List of doctors
    - Page for creating a new doctor
    - List of patients

__Error pages__

    - Unexpected error (code 500)
    - Page not found (code 404)
    
# How does it work?

__1. Login to the application__

To start working with the MedicalService application, the user needs to log in. To do this, you must fill out the form on the registration page or log in through the login page:

    Registration Page
![register](https://user-images.githubusercontent.com/70706111/169794481-c26622d4-1032-4e2c-9a0e-33366a8cfc3f.png)

    Login page
![login](https://user-images.githubusercontent.com/70706111/169794478-7292d385-f026-42b5-816a-bcc947389ad9.png)

__2. Patient registration__

When registering, the user is redirected to the patient registration page (entering the data necessary for further work in the profile)

    Patient Profile Registration Page
 ![3](https://user-images.githubusercontent.com/70706111/169796720-63720a36-fafa-483f-8f16-04da62c2ad9f.png)
    
__3. User pages in the role of "patient"__

After creating a profile, the user is added to the "patient" role and gets access to the Calendar and Patient Profile pages.

    Home page of the user in the role of "patient"
![4](https://user-images.githubusercontent.com/70706111/169796723-cf99af63-c99d-4cda-ac2d-805b8d46cd60.png)

    Patient Calendar
![5](https://user-images.githubusercontent.com/70706111/169796726-4e7ad0f5-70f4-4b2b-bbcf-9a275097d914.png)

    Display of medicines/procedures assigned to the patient
![6](https://user-images.githubusercontent.com/70706111/169796730-1cc80fd9-1e6f-4b52-9e6a-544427418842.png)

    Patient profile (editable)
![7](https://user-images.githubusercontent.com/70706111/169796732-1d190f85-aeae-47ec-9bda-8716bdb90dd2.png)

__4. User pages in the role of "doctor"__

The system user is added to the “doctor” role by the administrator. From the main page available to the doctor, you navigate to the Doctor Profile, Patient Profile (since a doctor can also become ill and act as a patient) and List of patients assigned to this doctor.

    Home page of the user in the role of "doctor"
![8](https://user-images.githubusercontent.com/70706111/169796735-bdfc730a-a812-4e3f-a9d4-545f8d05c747.png)

    Doctor profile (editable)
![9](https://user-images.githubusercontent.com/70706111/169796740-601f698d-f956-4e2a-aa22-db61aad56abf.png)

    List of doctor's patients
![10](https://user-images.githubusercontent.com/70706111/169796742-d9cefa76-d00d-4bd2-8c98-b1a8d4c871dd.png)    

    Creating a doctor's prescription for a patient (in the selected patient's calendar)
![11](https://user-images.githubusercontent.com/70706111/169796745-28c1c618-822c-49bd-9cfb-c7a6443ffac9.png)

__5. User pages in the role of "administrator"__

A user in the role of "administrator" is created in the database automatically when the application is first launched (if the database was initially empty) and has access to all pages available to users in the roles of "patient" and "doctor" with the ability to make changes on these pages, if necessary .
The administrator also has access to the following pages: List of system users, Creating a new user, Adding/deleting a user to/from a role, List of patients (access to calendars and user profiles), List of doctors (access to their patient lists, work profiles), Adding new doctor.

    Home page of the user in the role of "administrator"
![12](https://user-images.githubusercontent.com/70706111/169796747-f331b40e-df2b-4f50-a8b0-e20236f44841.png)

    List of system users with the ability to filter and search
![13](https://user-images.githubusercontent.com/70706111/169796749-d7acf52e-a8b5-4221-8c7a-c2e8b063a36e.png)

    List of doctors with the ability to filter and search
![14](https://user-images.githubusercontent.com/70706111/169796751-1a04ab8d-92b1-477e-84c0-5ebb48252a43.png)

    Page for creating a new doctor
![15](https://user-images.githubusercontent.com/70706111/169796755-52177de1-9c0a-45a8-851b-1f0ebaef8aac.png)
