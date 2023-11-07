pipeline{
	agent any
	triggers{
		pollSCM("* * * * *")
	}
	stages{
		stage("Build"){
			steps{
				sh "docker compose build"
			}
		}
		stage("Prepare services"){
			steps{
				sh "docker compose up add-service"
				sh "docker compose up sub-service"
				sh "docker compose up gateway-service"

				sh "docker compose up history-service"
				sh "docker compose up history-db"
			}
		}
		stage("Test"){
			steps{
				echo "Test not yet implemented"
			}
		}
		stage("Deliver"){
			steps{
				echo "Deliver not yet implemented"
			}
		}
	}
}