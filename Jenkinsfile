pipeline{
	agent any
	triggers{
		pollSCM("* * * * *")
	}
	stages{
		stage('Build'){
			steps{
				echo "Build"
				sh 'history-service'
			}
		}
		stage("Prepare services"){
			steps{
				echo "Prepare services
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