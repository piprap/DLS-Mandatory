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
				sh 'docker compose up -d'
				echo "add-service up and running echo"
			}
		}
		stage("Test"){
			steps{
				echo "Running Newman tests echo"
				echo "Installing Newman..."
				sh 'npm install -g newman'
				echo "Running Newman tests..."
				sh 'pwd'
				sh 'ls'
				sh 'newman run /c/Users/londo/.jenkins/workspace/Compulsory3/Postman/DLSMandatory3.postman_collection.json'
			}
		}
		stage("Deliver"){
			steps{
				withCredentials([usernamePassword(credentialsId: 'DockerHub' , usernameVariable: 'USERNAME', passwordVariable:'PASSWORD')]){
					sh 'docker login -u $USERNAME -p $PASSWORD'
					sh 'docker compose push add-service'

				}
			}
		}
	}
}