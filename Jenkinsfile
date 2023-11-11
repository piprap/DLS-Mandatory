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
				echo "Running Newman tests echo"
				sh 'docker run -t -v ${PWD}:/Postman postman/newman ls /Postman'
				sh 'docker run -t -v ${PWD}:/Postman postman/newman run /Postman/DLSMandatory3.postman_collection.json'
			}
		}
		stage("Deliver"){
			steps{
				echo "Deliver not yet implemented echo"
			}
		}
	}
}