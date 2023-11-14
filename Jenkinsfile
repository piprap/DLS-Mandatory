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
		stage("test cleanup"){
			steps{
				sh 'docker compose down'
			}
			
		}
		
		stage("Deliver"){
			steps{
				withCredentials([usernamePassword(credentialsId: 'DockerHub' , usernameVariable: 'USERNAME', passwordVariable:'PASSWORD')]){
					sh 'docker login -u $USERNAME -p $PASSWORD'
					sh 'docker image list'
					
					sh 'docker tag compulsory3-add-service longhairy/calc-service:compulsory3-add-service'
					sh 'docker push longhairy/calc-service:compulsory3-add-service'
					
					sh 'docker tag compulsory3-sub-service longhairy/calc-service:compulsory3-sub-service'
					sh 'docker push longhairy/calc-service:compulsory3-sub-service'
					
					sh 'docker tag compulsory3-multi-service longhairy/calc-service:compulsory3-multi-service'
					sh 'docker push longhairy/calc-service:compulsory3-multi-service'
					
					sh 'docker tag compulsory3-history-service longhairy/calc-service:compulsory3-history-service'
					sh 'docker push longhairy/calc-service:compulsory3-history-service'
					
					sh 'docker tag compulsory3-frontend-service longhairy/calc-service:compulsory3-frontend-service'
					sh 'docker push longhairy/calc-service:compulsory3-frontend-service'
					
					sh 'docker tag compulsory3-gateway-service longhairy/calc-service:compulsory3-gateway-service'
					sh 'docker push longhairy/calc-service:compulsory3-gateway-service'
					
				}
			}
		}
		stage("Deploy"){
			steps{
				sh 'docker compose up -d'
			}
			
		}
		
	}
}