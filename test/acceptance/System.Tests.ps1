# TODO support docker-compose step in CI to run system tests

Set-StrictMode -Version Latest

Describe 'System Tests' -Tags 'Acceptance' , 'Quality' , 'Docker' {

    function HttpGetRequest {
        param (
            $relativeUrl
        )

        $sutHost = "127.0.0.1"

        $mockServer = "http://${sutHost}:8080"

        $url = "${mockServer}/${relativeUrl}"

        Invoke-WebRequest -Uri $url -UseBasicParsing -Verbose
    }

    Context "Docker-Compose" {

        BeforeAll {

            Push-Location

            Set-Location .\docker

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

        AfterAll {

            Pop-Location
        }

        It "Should start services" {

            docker ps -q |
                Measure-Object |
                Select-Object -ExpandProperty Count |
                    Should -BeGreaterOrEqual 1
        }

        Context "Mock Server" {

            $adminPanelRelativeUrl = '__admin/'

            It "Admin panel is available" {

                { HttpGetRequest $adminPanelRelativeUrl } | Should -Not -Throw
            }

            It "Mappings have been configured" {

                HttpGetRequest $adminPanelRelativeUrl |
                    ConvertFrom-Json |
                    Select-Object -ExpandProperty mappings |
                    Measure-Object |
                    Select-Object -ExpandProperty Count |
                        Should -BeGreaterOrEqual 1
            }
        }

        Context "HubSpot API" {

            It "Blogs" {

                { HttpGetRequest 'content/api/v2/blogs' } |
                    Should -Not -Throw
            }

            It "Blog Posts" {

                { HttpGetRequest 'content/api/v2/blog-posts' } |
                    Should -Not -Throw
            }

            It "Blog Topics" {

                { HttpGetRequest 'blogs/v3/topics' } |
                    Should -Not -Throw
            }

            It "Broadcasts" {

                { HttpGetRequest 'broadcast/v1/broadcasts' } |
                    Should -Not -Throw
            }

            It "Broadcast Channel Settings" {

                { HttpGetRequest 'broadcast/v1/channels/setting/publish/current' } |
                    Should -Not -Throw
            }

            It "Calendar Events" {

                { HttpGetRequest 'calendar/v1/events/task' } |
                    Should -Not -Throw
            }

            It "Companies Paged" {

                { HttpGetRequest 'companies/v2/companies/paged' } |
                    Should -Not -Throw
            }

            It "Company Contacts" {

                { HttpGetRequest 'companies/v2/companies/686724251/contacts' } |
                    Should -Not -Throw
            }

            It "Company Properties" {

                { HttpGetRequest 'properties/v1/companies/properties' } |
                    Should -Not -Throw
            }

            It "Contacts All" {

                { HttpGetRequest 'contacts/v1/lists/all/contacts/all' } |
                    Should -Not -Throw
            }

            It "Contacts List Dynamic" {

                { HttpGetRequest 'contacts/v1/lists/dynamic' } |
                    Should -Not -Throw
            }

            It "Contacts List Static" {

                { HttpGetRequest 'contacts/v1/lists/static' } |
                    Should -Not -Throw
            }

            It "Contacts Properties" {

                { HttpGetRequest 'properties/v1/contacts/properties' } |
                    Should -Not -Throw
            }

            It "CRM Deal Associations Paged" {

                { HttpGetRequest 'crm-associations/v1/associations/123/SomeThing/456' } |
                    Should -Not -Throw
            }

            It "CRM Line Items Paged" {

                { HttpGetRequest 'crm-objects/v1/objects/line_items/paged' } |
                    Should -Not -Throw
            }

            It "CRM Products Paged" {

                { HttpGetRequest 'crm-objects/v1/objects/products/paged' } |
                    Should -Not -Throw
            }

            It "CRM Tickets Paged" {

                { HttpGetRequest 'crm-objects/v1/objects/tickets/paged' } |
                    Should -Not -Throw
            }

            It "Deals Paged" {

                { HttpGetRequest 'deals/v1/deal/paged' } |
                    Should -Not -Throw
            }

            It "Deals Pipelines" {

                { HttpGetRequest 'deals/v1/pipelines' } |
                    Should -Not -Throw
            }

            It "Deals Properties" {

                { HttpGetRequest 'properties/v1/deals/properties' } |
                    Should -Not -Throw
            }

            It "Deals Recently Created" {

                { HttpGetRequest 'deals/v1/deal/recent/created' } |
                    Should -Not -Throw
            }

            It "Deals Recently Modified" {

                { HttpGetRequest 'deals/v1/deal/recent/modified' } |
                    Should -Not -Throw
            }

            It "Domains" {

                { HttpGetRequest 'content/api/v4/domains' } |
                    Should -Not -Throw
            }

            It "Email SMTP Tokens" {

                { HttpGetRequest 'email/public/v1/smtpapi/tokens' } |
                    Should -Not -Throw
            }

            It "Engagement Associated Company Paged" {

                { HttpGetRequest 'engagements/v1/engagements/associated/COMPANY/686724251/paged' } |
                    Should -Not -Throw
            }

            It "Engagement Associated Contact Paged" {

                { HttpGetRequest 'engagements/v1/engagements/associated/CONTACT/123/paged' } |
                    Should -Not -Throw
            }

            It "Engagement Associated Deal Paged" {

                { HttpGetRequest 'engagements/v1/engagements/associated/DEAL/12345678/paged' } |
                    Should -Not -Throw
            }

            It "Engagement Associated Object Type Paged" {

                { HttpGetRequest 'engagements/v1/engagements/associated/ABC/123456/paged' } |
                    Should -Not -Throw
            }

            It "Engagements Paged" {

                { HttpGetRequest 'engagements/v1/engagements/paged' } |
                    Should -Not -Throw
            }

            It "FileManager Files" {

                { HttpGetRequest 'filemanager/api/v2/files' } |
                    Should -Not -Throw
            }

            It "Forms" {

                { HttpGetRequest 'forms/v2/forms' } |
                    Should -Not -Throw
            }

            It "Line Items Properties" {

                { HttpGetRequest 'properties/v2/line_items/properties' } |
                    Should -Not -Throw
            }

            It "Organization" {

                { HttpGetRequest 'Organization' } |
                    Should -Not -Throw
            }

            It "Owners" {

                { HttpGetRequest 'owners/v2/owners' } |
                    Should -Not -Throw
            }

            It "Settings" {

                { HttpGetRequest 'integrations/v1/me' } |
                    Should -Not -Throw
            }

            It "Site Maps" {

                { HttpGetRequest 'content/api/v2/site-maps' } |
                    Should -Not -Throw
            }

            It "Social Calendar Events" {

                { HttpGetRequest 'calendar/v1/events/social?hapikey=demo&startDate=15102018&endDate=30102018' } |
                    Should -Not -Throw
            }

            It "Tables" {

                { HttpGetRequest 'hubdb/api/v2/tables' } |
                    Should -Not -Throw
            }

            It "Table Rows" {

                { HttpGetRequest 'hubdb/api/v2/tables/300081/rows?portalId=62515' } |
                    Should -Not -Throw
            }
            It "Templates" {

                { HttpGetRequest 'content/api/v2/templates' } |
                    Should -Not -Throw
            }

            It "Tickets Properties" {

                { HttpGetRequest 'properties/v2/tickets/properties' } |
                    Should -Not -Throw
            }

            It "URL Mappings" {

                { HttpGetRequest 'url-mappings/v3/url-mappings' } |
                    Should -Not -Throw
            }

            It "Workflows" {

                { HttpGetRequest 'automation/v3/workflows' } |
                    Should -Not -Throw
            }
        }
    }
}
