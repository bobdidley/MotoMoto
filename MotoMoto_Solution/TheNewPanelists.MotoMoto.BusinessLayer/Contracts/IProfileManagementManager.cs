﻿using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public interface IProfileManagementManager
    {
        public ProfileModel CreateProfilesForAllNewAccountsManager();
        public ProfileListModel RetrieveAllProfileManager();
        public ProfileModel RetrieveSpecifiedProfileManager(string _username);
        public ProfileModel RetrieveAllUpVotedPostsForProfileManager(string _username);
        public ProfileModel RetrieveSpecifiedUserPostsManager(string _username);
        public ProfileModel UpdateProfileDescriptionManager(string _username, string _newDescription);
        public ProfileModel UpdateProfileUsernameManager(string _username, string _newUsername);
        public ProfileModel UpdateProfileStatusManager(string _username, bool _status);
        public ProfileModel UpdateProfileImageManager(string _username, string _newImageURL);
        public ProfileModel DeleteProfileManager(string _username);

    }
}