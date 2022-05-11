﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class EventListService
    {
        // Readonly means that the object/variable cannot be defined outside of the
        // constructor
        private readonly EventPostContentDataAccess _eventPostContentDAO;
        
        // Single argument constructor
        public EventListService(EventPostContentDataAccess eventPostContentDataAccess){ _eventPostContentDAO = eventPostContentDataAccess; }

        // Function to FetchAllEventPosts 
        public ISet<EventDetailsModel> FetchAllEventPosts() // NOTE: Might not need passed in arg because not being used
        {
            // Use the DAO object to retrieve all rows from the EventDetails table and store it in a HashSet
            var eventDetailsEntities = _eventPostContentDAO.FetchAllPosts();

            // Selects each row from the retrieved HashSet and stores it 
            var events = eventDetailsEntities!.Select(evnt => new EventDetailsModel()
            {
                eventID = evnt!.eventID,
                eventCity = evnt!.eventCity,
                eventTime = evnt!.eventTime,
                eventDate = evnt!.eventDate
            }).ToHashSet();
            return events; // Returns the retrieved data back to the manager
        }

        public ISet<ProfileModel> FetchAllEventAccounts() 
        {
            // Use the DAO object to retrieve all rows from the EventDetails table and store it in a HashSet
            var profileEntity = _eventPostContentDAO.FetchAllEventAccounts();

            // Selects each row from the retrieved HashSet and stores it 
            var profile = profileEntity!.Select(profile => new ProfileModel()
            {
                username = profile!.username
            }).ToHashSet();
            return profile; // Returns the retrieved data back to the manager
        }

        /// <summary>
        /// Using the passed in EventDetailsModel
        /// Insert the validated user inputted values into the datastore
        /// </summary>
        /// <param name="eventDetailsModel"></param>
        /// <returns></returns>
        public EventDetailsModel CreateEventPost(EventDetailsModel eventDetailsModel)
        {
            EventDetailsModel eventModel = eventDetailsModel;
            try
            {
                eventModel = _eventPostContentDAO.CreateEventPost(eventModel);
            }
            catch
            {
                return eventModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return eventModel.GetResponse(ResponseModel.response.success);
        }


        public EventAccountVerificationModel CreateReview(EventAccountVerificationModel eventAccountVerificationModel)
        {
            EventAccountVerificationModel eventAccountModel = eventAccountVerificationModel;
            try
            {
                eventAccountModel = _eventPostContentDAO.CreateReview(eventAccountModel);
            }
            catch
            {
                return eventAccountModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return eventAccountModel.GetResponse(ResponseModel.response.success);
        }

        public ISet<EventAccountVerificationModel> FetchAllReviews(string username)
        {
            // Use the DAO object to retrieve all rows from the EventDetails table and store it in a HashSet
            var eventAccountEntity = _eventPostContentDAO.FetchAllReviews(username);

            // Selects each row from the retrieved HashSet and stores it 
            var reviews = eventAccountEntity!.Select(account => new EventAccountVerificationModel()
            {
                username = account!.username,
                rating = account!.rating,
                review = account!.review,
            }).ToHashSet();
            return reviews; // Returns the retrieved data back to the manager
        }
    }
}
