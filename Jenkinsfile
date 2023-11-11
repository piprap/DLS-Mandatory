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
				sh 'docker compose up add-service'
				
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