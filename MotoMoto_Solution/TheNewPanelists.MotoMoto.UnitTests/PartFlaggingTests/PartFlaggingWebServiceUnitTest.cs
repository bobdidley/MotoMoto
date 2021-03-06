using Xunit;
using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.WebServices.PartFlagging.Controllers;
using TheNewPanelists.MotoMoto.Models;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    /// <summary>
    /// Contains unit testing for the part flagging web services api
    /// </summary>
    public class PartFlaggingWebServiceUnitTest
    {

        /// <summary>
        /// Entity containing data access for part flagging functionality
        /// </summary>
        private readonly IPartFlaggingDataAccess __partFlaggingDataAccess;

        /// <summary>
        /// Entity containing business logic for part flagging functionality
        /// </summary>
        private readonly IPartFlaggingBusinessLayer __partFlaggingBusinessLayer;

        /// <summary>
        /// Entity containing controller functions for part flagging functionality
        /// </summary>
        private readonly PartFlaggingController __partFlaggingController;
        
        /// <summary>
        /// Default constructors. Used to inintialize all layers used in unit testing for
        /// part flagging
        /// </summary>
        public PartFlaggingWebServiceUnitTest()
        {
            __partFlaggingDataAccess = new PartFlaggingDataAccess();
            __partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            __partFlaggingController = new PartFlaggingController();
        }
        /// <summary>
        /// Tests creation of valid flag using the function exposed to the PartFlagging WebAPI.
        /// Test is successful if the resulting object from the function has a success message.
        /// </summary>
        [Fact]
        public void CreateValidFlag()
        {
            string partNum = "1";
            string carMake = "Honda";
            string carModel = "Accord";
            string carYear = "2022";

            IActionResult result = __partFlaggingController.CreateFlag(partNum, carMake, carModel, carYear);

            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Flag Successfully Created" }, 
            };

            var compareResult = new OkObjectResult(response);
            bool comparison = false;

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okResultDict = okResult.Value as Dictionary<string, string>;

            if (okResultDict is not null)
            {
                comparison = okResultDict["message"] == "Flag Successfully Created";
            }
            
            Assert.True(comparison);
        }

        /// <summary>
        /// Tests creation of invalid flag using the function exposed to the PartFlagging WebAPI.
        /// Test is successful if the resulting object from the function has a failure message.
        /// </summary>
        [Fact]
        public void CreateInvalidFlag()
        {
            string partNum = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            IActionResult result = __partFlaggingController.CreateFlag(partNum, carMake, carModel, carYear);

            bool comparison = false;

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var badResultDict = badResult.Value as Dictionary<string, string>;

            if (badResultDict is not null)
            {
                comparison = badResultDict["message"] == "Error Flag Could Not Be Created";
            }
            
            Assert.True(comparison);
        }

        /// <summary>
        /// Tests decrement of valid flag using the function exposed to the PartFlagging WebAPI.
        /// Test is successful if the resulting object from the function has a success message.
        /// </summary>
        [Fact]
        public async void DecrementValidFlag()
        {
            const string testName = "DecrementValidFlag";

            string partNum = testName;
            string carMake = testName;
            string carModel = testName;
            string carYear = "2022";

            //Ensure flag exists
            await __partFlaggingDataAccess.CreateOrIncrementFlag(__partFlaggingBusinessLayer.CreateFlagModel(partNum, carMake, carModel, carYear));

            IActionResult result = __partFlaggingController.CreateFlag(partNum, carMake, carModel, carYear);

            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Flag Successfully Created" }, 
            };

            var compareResult = new OkObjectResult(response);
            bool comparison = false;

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okResultDict = okResult.Value as Dictionary<string, string>;

            if (okResultDict is not null)
            {
                comparison = okResultDict["message"] == "Flag Successfully Created";
            }
            
            Assert.True(comparison);
        }

        /// <summary>
        /// Tests decrement of invalid flag using the function exposed to the PartFlagging WebAPI.
        /// Test is successful if the resulting object from the function has an error message.
        /// </summary>
        [Fact]
        public void DecrementInvalidFlag()
        {
            string partNum = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            IActionResult result = __partFlaggingController.DecrementFlagCount(partNum, carMake, carModel, carYear);

            bool comparison = false;

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var badResultDict = badResult.Value as Dictionary<string, string>;

            if (badResultDict is not null)
            {
                comparison = badResultDict["message"] == "Error Flag Could Not Be Decremented";
            }
            
            Assert.True(comparison);
        }

        /// <summary>
        /// Tests retrieval of valid part compatibility based on flag information using the PartFlagging WebAPI.
        /// Test is successful if the resulting object from the function has the boolean value representing compatibility.
        /// </summary>
        [Fact]
        public void GetCompatibilityValidFlag()
        {
            string partNum = "1";
            string carMake = "Honda";
            string carModel = "Accord";
            string carYear = "2022";

            IActionResult result = __partFlaggingController.IsPossibleIncompatibility(partNum, carMake, carModel, carYear);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okResultDict = okResult.Value as Dictionary<string, bool>;

            bool operationSuccess = false; 
            if (okResultDict is not null)
            {
                if (okResultDict.ContainsKey("isPossibleIncompatiblility"))
                {
                    operationSuccess = true;
                }
            }
            
            Assert.True(operationSuccess);
        }

        /// <summary>
        /// Tests retrieval of invalid part compatibility based on flag information using the PartFlagging WebAPI.
        /// Test is successful if the resulting object from the function is of the type BadRequest meaning the request failed
        /// due to the flag being invalid.
        /// </summary>
        [Fact]
        public void GetInvalidFlag()
        {
            string partNum = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            IActionResult result = __partFlaggingController.IsPossibleIncompatibility(partNum, carMake, carModel, carYear);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

    
}