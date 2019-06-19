# TODO support docker-compose step in CI to run system tests

Set-StrictMode -Version Latest

Describe 'System Tests' -Tags 'Acceptance' , 'Quality' {

    Context "Docker-Compose" {

        $sutHost = "127.0.0.1"

        $mockServer = "http://${sutHost}:8080"
        
        BeforeAll {

            docker-compose build

            docker-compose up -d --remove-orphans

            $systemStarted = 'false'

            Do {

                try {
                    docker inspect (docker-compose ps -q wiremock)  
                    $systemStarted = 'true '
                }
                catch {
                    Write-Host 'waiting ...'
                }

            } While ($systemStarted -eq 'false')
        }

        It "Should start services" {

            docker ps -q | 
                Measure-Object | 
                Select-Object -ExpandProperty Count | 
                    Should -BeGreaterOrEqual 1
        }

        Context "Mock Server" {
        
            It "Root folder access is forbidden" {

                { Invoke-WebRequest $mockServer } | Should -Throw 403
            }

            It "Admin panel is available" {

                { Invoke-WebRequest "$mockServer/__admin/" -UseBasicParsing } | Should -Not -Throw
            }
            
            It "Mappings have been configured" {

                Invoke-WebRequest "$mockServer/__admin/" -UseBasicParsing | 
                    ConvertFrom-Json | 
                    Select-Object -ExpandProperty mappings | 
                    Measure-Object | 
                    Select-Object -ExpandProperty Count | 
                        Should -BeGreaterOrEqual 1
            }
        }

        Context "HubSpot API" {        

            It "Companies Paged" {

                { Invoke-WebRequest "$mockServer/companies/v2/companies/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Company Contacts" {

                { Invoke-WebRequest "$mockServer/companies/v2/companies/686724251/contacts" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Company Properties" {

                { Invoke-WebRequest "$mockServer/properties/v1/companies/properties" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Contacts All" {

                { Invoke-WebRequest "$mockServer/contacts/v1/lists/all/contacts/all" -UseBasicParsing } | 
                    Should -Not -Throw
            }        

            It "Contacts List Dynamic" {

                { Invoke-WebRequest "$mockServer/contacts/v1/lists/dynamic" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Contacts List Static" {

                { Invoke-WebRequest "$mockServer/contacts/v1/lists/static" -UseBasicParsing } | 
                    Should -Not -Throw
            }            

            It "Contacts Properties" {

                { Invoke-WebRequest "$mockServer/properties/v1/contacts/properties" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "CRM Deal Associations Paged" {

                { Invoke-WebRequest "$mockServer/crm-associations/v1/associations/123/SomeThing/456" -UseBasicParsing } | 
                    Should -Not -Throw
            }
            
            It "CRM Line Items Paged" {

                { Invoke-WebRequest "$mockServer/crm-objects/v1/objects/line_items/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "CRM Products Paged" {

                { Invoke-WebRequest "$mockServer/crm-objects/v1/objects/products/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }
            
            It "CRM Tickets Paged" {

                { Invoke-WebRequest "$mockServer/crm-objects/v1/objects/tickets/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Deals Paged" {

                { Invoke-WebRequest "$mockServer/deals/v1/deal/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Deals Pipelines" {

                { Invoke-WebRequest "$mockServer/deals/v1/pipelines" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Deals Properties" {

                { Invoke-WebRequest "$mockServer/properties/v1/deals/properties" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Deals Recently Created" {

                { Invoke-WebRequest "$mockServer/deals/v1/deal/recent/created" -UseBasicParsing } | 
                    Should -Not -Throw
            }
            
            It "Deals Recently Modified" {

                { Invoke-WebRequest "$mockServer/deals/v1/deal/recent/modified" -UseBasicParsing } | 
                    Should -Not -Throw
            }
                
            It "Engagement Associated Company Paged" {

                { Invoke-WebRequest "$mockServer/engagements/v1/engagements/associated/COMPANY/686724251/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Engagement Associated Contact Paged" {

                { Invoke-WebRequest "$mockServer/engagements/v1/engagements/associated/CONTACT/123/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Engagement Associated Deal Paged" {

                { Invoke-WebRequest "$mockServer/engagements/v1/engagements/associated/DEAL/12345678/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Engagement Associated Object Type Paged" {

                { Invoke-WebRequest "$mockServer/engagements/v1/engagements/associated/ABC/123456/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Engagements Paged" {

                { Invoke-WebRequest "$mockServer/engagements/v1/engagements/paged" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "FileManager Files" {

                { Invoke-WebRequest "$mockServer/filemanager/api/v2/files" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Forms" {

                { Invoke-WebRequest "$mockServer/forms/v2/forms" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Line Items Properties" {

                { Invoke-WebRequest "$mockServer/properties/v2/line_items/properties" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Organization" {

                { Invoke-WebRequest "$mockServer/Organization" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Owners" {

                { Invoke-WebRequest "$mockServer/owners/v2/owners" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Settings" {

                { Invoke-WebRequest "$mockServer/integrations/v1/me" -UseBasicParsing } | 
                    Should -Not -Throw
            }  
            
            # TODO test Tables , Blogs , Workflows , Templates

            # TODO test SocialCalendarEvents ... https://api.hubapi.com/calendar/v1/events/social?hapikey=demo&startDate=15102018&endDate=30102018

            It "Tickets Properties" {

                { Invoke-WebRequest "$mockServer/properties/v2/tickets/properties" -UseBasicParsing } | 
                    Should -Not -Throw
            }             
        }
    }
}
