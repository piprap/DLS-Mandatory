pipeline{
	agent any
	triggers{
		pollSCM("* * * * *")
	}
	stages{
		stage('Build'){
			steps{
				echo "Build echo"
				sh 'docker compose build'
			}
		}
		stage("Prepare services"){
			steps{
				echo "Prepare services echo"
				echo "Starting add-service echo"
				sh 'docker compose up -d add-service'
				echo "add-service up and running echo"
				
			}
		}
		stage("Test"){
			steps{
				echo "Test not yet implemented echo"
			}
		}
		stage("Deliver"){
			steps{
				echo "Deliver not yet implemented echo"
			}
		}
	}
}