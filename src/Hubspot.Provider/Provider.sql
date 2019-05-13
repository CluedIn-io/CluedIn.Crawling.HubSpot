USE OpenCommunication
GO
INSERT [dbo].[Provider] ([ID], [Name], [Active],[Plan], [Details], [Type], [Logo], [PullLink], [Category], [Configuration])
VALUES ('2d3cf71a-8d29-4ac0-9864-e413f3c7246d', N'HubSpot', 1, 1, N'Our HubSpot provider will allow you to search across all your HubSpot accounts.', N'cloud', N'http://immense-refuge-3500.herokuapp.com/img/providers/salesforce.png', N'http://proget.cerebro.technology/salesforce', 'Files', '{ "actions": [ { "name" : "start", "action": "javascript function"}, { "name" : "share", "action": "javascript function for share"} ] }')
GO
