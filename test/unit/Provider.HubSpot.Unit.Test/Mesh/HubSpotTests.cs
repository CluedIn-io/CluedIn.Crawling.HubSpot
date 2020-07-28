//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;

//using Castle.MicroKernel.Registration;

//using CluedIn.Core;
//using CluedIn.Core.Data;
//using CluedIn.Core.Data.Parts;
//using CluedIn.Core.Data.Vocabularies;
//using CluedIn.Core.Messages.Processing;
//using CluedIn.Core.Models;
//using CluedIn.Core.Processing;
//using CluedIn.Core.Serialization;
//using CluedIn.Core.Workflows;
//using CluedIn.ExternalSearch;
//using CluedIn.ExternalSearch.Providers.ClearBit;
//using CluedIn.ExternalSearch.Providers.CompanyHouse;
//using CluedIn.ExternalSearch.Providers.CVR;
//using CluedIn.ExternalSearch.Providers.FacebookGraph;
//using CluedIn.ExternalSearch.Providers.KnowledgeGraph;
//using CluedIn.Processing;
//using CluedIn.Processing.Actors;
//using CluedIn.Processing.EntityResolution;
//using CluedIn.Processing.Models;
//using CluedIn.Processing.Processors.PreProcessing;
//using CluedIn.Processing.Processors.Specific;

//using Moq;

//using Xunit;
//using CluedIn.Processing.Services;
//using CluedIn.Core.Services;
//using CluedIn.ExternalSearch.Providers.FullContact;
//using CluedIn.Providers.Mesh;
//using CluedIn.Core.Mesh;
//using CluedIn.Crawling.HubSpot;
//using CluedIn.Core.DataStore;
//using CluedIn.Core.Messages.WebApp;
//using CluedIn.Core.Data.Relational;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//using CluedIn.Core.Accounts;

//namespace CluedIn.Tests.Unit.ExternalSearch
//{
//    [TestFixture]
//    public class HubSpotTests
//    {
//        /**********************************************************************************************************
//         * FIELDS
//         **********************************************************************************************************/

//        private TestContext testContext;

//        /**********************************************************************************************************
//         * SETUP
//         **********************************************************************************************************/

//        [OneTimeSetUp]
//        public void TestSetup()
//        {

//        }

//        /**********************************************************************************************************
//         * TESTS
//         **********************************************************************************************************/

//        [Fact]
//        public void Test()
//        {
//            // Arrange
//            this.testContext = new TestContext();

//            var meshQuery = new MeshQuery()
//            {
//                Action = ActionType.UPDATE,
//                DisconnectAccountAndRemoveFromCluedIn = false,
//                IsQueued = true,
//                RemoveFromCluedIn = false,
//                Query = "{100919BF-2B82-4F87-A292-65FEF8E35B75}",
//                Transform = new List<Transform>()
//                {
//                    new Transform()
//                    {
//                        key = "cluedin.user.jobTitle",
//                        value = "CTO"
//                    }
//                }
//            };

//            var dictionary = new Dictionary<string, object>();

//            var crawlJobData = new HubSpotCrawlJobData(dictionary);
//            crawlJobData.ApiToken = "sfsdfsdfsfs";

//            IEntityMetadata entityMetadata = new EntityMetadataPart()
//            {
//                Name = "CluedIn",
//                EntityType = EntityType.Organization
//            };

//            var e = new Entity(EntityType.Organization, this.testContext.Context.Organization);
//            e.Codes.Add(new EntityCode(EntityType.Organization, "HubSpot", "123"));

//            var hubspotMeshProvider = new Mock<HubSpotDealMeshProcessor>(MockBehavior.Loose, this.testContext.AppContext.Object);

//            hubspotMeshProvider.CallBase = true;

//            var configurationRepo = new Mock<IConfigurationRepository>(MockBehavior.Loose);
//            var messageRepo = new Mock<IRelationalDataStore<GDPRMessage>>(MockBehavior.Loose);
//            var providerDefinitionRepo = new Mock<IRelationalDataStore<ProviderDefinition>>(MockBehavior.Loose);
//            var providers = new Mock<IOrganizationProviders>(MockBehavior.Loose);
//            var entity = new Mock<IPrimaryEntityDataStore<Entity>>(MockBehavior.Loose);

//            providers.Setup(foo => foo.GetProviderDefinition(It.IsAny<ExecutionContext>(), It.IsAny<Guid>())).Returns(new ProviderDefinition() { Id = new Guid("{3C7D366D-66A0-4382-9DC5-6FF8BF9D9D94}") });
//            configurationRepo.Setup(foo => foo.GetConfigurationById(It.IsAny<ExecutionContext>(), It.IsAny<Guid>())).Returns(dictionary);
//            entity.Setup(foo => foo.GetById(It.IsAny<ExecutionContext>(), It.IsAny<Guid>())).Returns(e);


//            this.testContext.Container.Register(Component.For<IMeshProcessor>().UsingFactoryMethod(() => hubspotMeshProvider.Object));
//            this.testContext.Container.Register(Component.For<IConfigurationRepository>().UsingFactoryMethod(() => configurationRepo.Object));
//            this.testContext.Container.Register(Component.For<IRelationalDataStore<GDPRMessage>>().UsingFactoryMethod(() => messageRepo.Object));
//            this.testContext.Container.Register(Component.For<IRelationalDataStore<ProviderDefinition>>().UsingFactoryMethod(() => providerDefinitionRepo.Object));
//            this.testContext.Container.Register(Component.For<HubSpotCrawlJobData>().UsingFactoryMethod(() => crawlJobData));

//            var context = this.testContext.Context.ToProcessingContext();
//            var command = new MeshDataCommand();
//            var actor = new MeshDataProcessingPipeline(this.testContext.AppContext.Object);
//            var workflow = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<MeshDataCommand>());

//            workflow.CallBase = true;

//            command.OrganizationId = context.Organization.Id;
//            command.HttpPostData = JsonUtility.Serialize(meshQuery);
//            command.ProviderId = Constants.Providers.HubSpotId;
//            command.ProviderDefinitionId = new Guid("{3C7D366D-66A0-4382-9DC5-6FF8BF9D9D94}");
//            command.Workflow = workflow.Object;
//            command.JobId = Guid.NewGuid();
//            context.Workflow = command.Workflow;

//            // Act
//            var result = actor.ProcessWorkflowStep(context, command);
//            Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

//            result = actor.ProcessWorkflowStep(context, command);
//            Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
//            context.Workflow.AddStepResult(result);

//            context.Workflow.ProcessStepResult(context, command);

//            // Assert
//            //Verify that the call is turned to not queued
//            Assert.False(command.IsQueued);

//            //Verify that a Service Bus message is sent
//            this.testContext.AppContext.Verify(v => v.System.ServiceBus.Publish<Core.Messages.WebApp.MeshQueueMessageCommand>(It.IsAny<MeshQueueMessageCommand>()), Times.Once);

//            //Verify that the Validate Method Runs and returns the right Status Code
//           // hubspotMeshProvider.Verify(v => v.Validate(this.testContext.Context, command, dictionary, meshQuery), Times.Once);

//            //Verify that this is logged.
//        }


//        [Fact]
//        public void MinimizeTest()
//        {
//            // Arrange
//            this.testContext = new TestContext();

//            var meshQuery = new MeshQuery()
//            {
//                Action = ActionType.UPDATE,
//                DisconnectAccountAndRemoveFromCluedIn = false,
//                IsQueued = true,
//                RemoveFromCluedIn = false,
//                Query = "{100919BF-2B82-4F87-A292-65FEF8E35B75}",
//                Transform = new List<Transform>()
//                {
//                    new Transform()
//                    {
//                        key = "cluedin.user.jobTitle",
//                        value = "CTO"
//                    }
//                }
//            };

//            var dictionary = new Dictionary<string, object>();

//            var crawlJobData = new HubSpotCrawlJobData(dictionary);
//            crawlJobData.ApiToken = "sfsdfsdfsfs";

//            IEntityMetadata entityMetadata = new EntityMetadataPart()
//            {
//                Name = "CluedIn",
//                EntityType = EntityType.Organization
//            };

//            var hubspotMeshProvider = new Mock<HubSpotDealMeshProcessor>(MockBehavior.Loose, this.testContext.AppContext.Object);

//            hubspotMeshProvider.CallBase = true;

//            var configurationRepo = new Mock<IConfigurationRepository>(MockBehavior.Loose);

//            //this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));

//            this.testContext.Container.Register(Component.For<IMeshProcessor>().UsingFactoryMethod(() => hubspotMeshProvider.Object));
//            this.testContext.Container.Register(Component.For<IConfigurationRepository>().UsingFactoryMethod(() => configurationRepo.Object));
//            this.testContext.Container.Register(Component.For<HubSpotCrawlJobData>().UsingFactoryMethod(() => crawlJobData));

//            var context = this.testContext.Context.ToProcessingContext();
//            var command = new MeshDataCommand();
//            var actor = new MeshDataProcessingPipeline(this.testContext.AppContext.Object);
//            var workflow = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<MeshDataCommand>());

//            workflow.CallBase = true;

//            command.OrganizationId = context.Organization.Id;
//            command.HttpPostData = JsonUtility.Serialize(meshQuery);
//            command.ProviderId = Constants.Providers.HubSpotId;
//            command.Workflow = workflow.Object;
//            command.JobId = Guid.NewGuid();
//            context.Workflow = command.Workflow;

//            // Act
//            var result = actor.ProcessWorkflowStep(context, command);
//            Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

//            result = actor.ProcessWorkflowStep(context, command);
//            Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
//            context.Workflow.AddStepResult(result);

//            context.Workflow.ProcessStepResult(context, command);

//            // Assert
//            //Verify that the call is turned to not queued
//            Assert.False(command.IsQueued);

//            //Verify that a Service Bus message is sent
//            this.testContext.AppContext.Verify(v => v.System.ServiceBus.Publish<Core.Messages.WebApp.MeshQueueMessageCommand>(It.IsAny<MeshQueueMessageCommand>()), Times.Once);

//            //Verify that the Validate Method Runs and returns the right Status Code
//          //  hubspotMeshProvider.Verify(v => v.Validate(this.testContext.Context, command, dictionary, meshQuery), Times.Once);

//            //Verify that this is logged.
//        }
//    }
//}
