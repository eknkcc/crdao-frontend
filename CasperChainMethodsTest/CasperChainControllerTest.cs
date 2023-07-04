using DAO_WebPortal.Controllers;
using Helpers.Models.IdentityModels;
using Helpers.Models.WebsiteViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CasperChainMethodsTest
{
    public class MockSession : ISession
    {
        private Dictionary<string, byte[]> _sessionData = new Dictionary<string, byte[]>();

        public bool IsAvailable => throw new NotImplementedException();

        public string Id => throw new NotImplementedException();

        public IEnumerable<string> Keys => throw new NotImplementedException();

        public void SetString(string key, string value)
        {
            _sessionData[key] = Encoding.UTF8.GetBytes(value);
        }

        public void SetInt32(string key, int value)
        {
            SetString(key, value.ToString());
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            return _sessionData.TryGetValue(key, out value);
        }

        public void Set(string key, byte[] value)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }


    [TestClass]
    public class CasperChainControllerTest
    {
        private CasperChainController controller = new CasperChainController();
        private MockSession mockSession;

        [TestInitialize]
        public void Setup()
        {
            // Create an instance of the controller with any dependencies (e.g., logger, providers)
            // Mock any dependencies using a mocking framework like Moq

            //Get related appsettings.json file (Development or Production)
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.json", optional: true)
                .AddEnvironmentVariables();

            mockSession = new MockSession();
            mockSession.SetInt32("UserID", 1);
            mockSession.SetString("Email", "eknkcc@gmail.com");
            mockSession.SetString("Token", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJBdXRob3JpemF0aW9uIjoiQXV0aG9yaXplZCIsIk5ld3NsZXR0ZXIiOiJGYWxzZSIsIklzQmxvY2tlZCI6IkZhbHNlIiwiRW1haWwiOiJla25rY2NAZ21haWwuY29tIiwiVXNlcklkIjoiMSIsIkFzc29jaWF0ZSI6IlRydWUiLCJuYmYiOjE2ODg0ODIxODUsImV4cCI6MTY4ODU2ODU4NX0.62uyHLad8NMkHSss4KCinVl9hDoHSvyRDnO9TM6F9gk");
            mockSession.SetString("LoginType", "user");
            mockSession.SetString("NameSurname", "test");
            mockSession.SetString("UserType", "VotingAssociate");
            mockSession.SetString("ProfileImage", "");
            mockSession.SetString("KYCStatus", "true");
            mockSession.SetString("WalletAddress", "01fa149016465812daa07838782d35bce84bafd844ffdd4433bc7d825702a7915e");
            mockSession.SetInt32("ChainSign", 1);

            DAO_WebPortal.Startup.LoadConfig(builder.Build());


            // Example: Mock the HttpContext and related properties
            var httpContext = new DefaultHttpContext();
            httpContext.Session = mockSession;
            var controllerContext = new ControllerContext { HttpContext = httpContext };

            //// Create an instance of the controller
            controller = new CasperChainController()
            {
                ControllerContext = controllerContext
            };
        }

        [TestMethod]
        public void GetUserChainProfile_ValidPublicAddress_ReturnsUserChainProfile()
        {
            // Arrange
            var publicAddress = "01fa149016465812daa07838782d35bce84bafd844ffdd4433bc7d825702a7915e";

            // Act
            var result = controller.GetUserChainProfile(publicAddress);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UserChainProfile));
        }


        [TestMethod]
        public void GetPostJobOfferDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var timeframe = 10L;
            var budget = 1000L;

            // Act
            var result = controller.GetPostJobOfferDeploy(timeframe, budget) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetSubmitBidDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var jobOfferId = 1U;
            var time = 10L;
            var userPayment = 1000UL;
            var repStake = 50UL;
            var onboard = true;

            // Act
            var result = controller.GetSubmitBidDeploy(jobOfferId, time, userPayment, repStake, onboard) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetCancelBidDeploy_ValidBidId_ReturnsJsonResult()
        {
            // Arrange
            var bidId = 1U;

            // Act
            var result = controller.GetCancelBidDeploy(bidId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetPickBidDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var jobId = 1U;
            var bidId = 2U;
            var bidAmount = 1000L;

            // Act
            var result = controller.GetPickBidDeploy(jobId, bidId, bidAmount) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetSubmitJobProofDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var jobId = 1U;
            var documentHash = "validDocumentHash";

            // Act
            var result = controller.GetSubmitJobProofDeploy(jobId, documentHash) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetSubmitJobProofGracePeriodDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var jobId = 1U;
            var proof = "validProof";
            var repStake = 50U;
            var onboard = true;

            // Act
            var result = controller.GetSubmitJobProofGracePeriodDeploy(jobId, proof, repStake, onboard) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetSubmitVoteDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var votingId = 313;
            var direction = 0;
            var stake = 100;

            // Act
            var result = controller.GetSubmitVoteDeploy(votingId, direction, stake) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetFinishVotingDeploy_ValidVotingId_ReturnsJsonResult()
        {
            // Arrange
            var votingId = 313;

            // Act
            var result = controller.GetFinishVotingDeploy(votingId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetSimpleVoteDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var documentHash = "validDocumentHash";
            var stake = 100;

            // Act
            var result = controller.GetSimpleVoteDeploy(documentHash, stake) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetVaOnboardingVoteDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var username = "validUsername";
            var vaAddress = "validVAAddress";
            var reason = "validReason";

            // Act
            var result = controller.GetVaOnboardingVoteDeploy(username, vaAddress, reason) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetRepoVoteDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var key = "validKey";
            var value = "validValue";
            var stake = 100;

            // Act
            var result = controller.GetRepoVoteDeploy(key, value, stake) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetKYCVoteDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var username = "validUsername";
            var stake = 100;

            // Act
            var result = controller.GetKYCVoteDeploy(username, stake) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetReputationVoteDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var action = 1;
            var subjectAddress = "validSubjectAddress";
            var amount = 50;
            var documentHash = "validDocumentHash";
            var stake = 100;
            var repUsername = "validRepUsername";

            // Act
            var result = controller.GetReputationVoteDeploy(action, subjectAddress, amount, documentHash, stake, repUsername) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetSlashingVoteDeploy_ValidParameters_ReturnsJsonResult()
        {
            // Arrange
            var addressToSlash = "validAddressToSlash";
            var slashRatio = 50;
            var stake = 100;
            var slashUsername = "validSlashUsername";

            // Act
            var result = controller.GetSlashingVoteDeploy(addressToSlash, slashRatio, stake, slashUsername) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
    }
}