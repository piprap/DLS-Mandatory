pipeline{
	agent any
	triggers{
		pollSCM("* * * * *")
	}
	stages{
		stage('Build'){
			agent{
				docker {image 'history-service'}
			}
			steps{
				echo "Build"
				
			}
		}
		stage("Prepare services"){
			steps{
				echo "Prepare services"
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