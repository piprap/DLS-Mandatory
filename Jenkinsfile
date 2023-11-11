pipeline{
	agent any
	triggers{
		pollSCM("* * * * *")
	}
	stages{
		stage('Build'){
			steps{
				echo "Build"
				sh 'docker compose build'
			}
		}
		stage("Prepare services"){
			steps{
				echo "Prepare services"
				echo "Starting add-service"
				sh 'docker compose up add-service'
				echo "add-service up and running"
				echo "Starting sub-service"
				sh 'docker compose up sub-service'
				echo "sub-service up and running"
				
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