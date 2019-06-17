# TODO support docker-compose step in CI to run system tests

Set-StrictMode -Version Latest

Describe 'System Tests' -Tags 'Acceptance' , 'Quality' {

    Context "Docker-Compose" {

        $sutHost = "127.0.0.1"

        $mockServer = "http://${sutHost}:8080"
        
        BeforeAll {

            docker-compose build

            docker-compose up -d --remove-orphans
        }

        It "Should start services" {

            docker ps -q | 
                Measure-Object | 
                Select-Object -ExpandProperty Count | 
                    Should -Be 1
        }

        Context "Mock Server" {
        
            It "Root folder access is forbidden" {

                { Invoke-WebRequest $mockServer } | Should -Throw 403
            }

            It "Admin panel is available" {

                { Invoke-WebRequest "$mockServer/__admin/" -UseBasicParsing } | Should -Not -Throw
            }
            
            It "Two Mappings have been configured" {

                Invoke-WebRequest "$mockServer/__admin/" -UseBasicParsing | 
                    ConvertFrom-Json | 
                    Select-Object -ExpandProperty mappings | 
                    Measure-Object | 
                    Select-Object -ExpandProperty Count | 
                        Should -Be 2
            }
        }

        Context "HubSpot API" {
        
            It "Settings" {

                { Invoke-WebRequest "$mockServer/integrations/v1/me" -UseBasicParsing } | 
                    Should -Not -Throw
            }

            It "Company Properties" {

                { Invoke-WebRequest "$mockServer/properties/v1/companies/properties" -UseBasicParsing } | 
                    Should -Not -Throw
            }
        }
    }
}
