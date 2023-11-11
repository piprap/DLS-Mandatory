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
					sh 'docker image list'
					
					services = ["add-service", "sub-service", "multi-service", "history-service", "frontend-service", "gateway-service"]

					for (service in services) {
						def imageTag = "${DOCKER_REPO_PREFIX}:${service}"
						sh "docker tag compulsory3-${service} ${imageTag}"
						sh "docker push ${imageTag}"
					}
					

					
				}
			}
		}
	}
}