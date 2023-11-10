pipeline{
	agent any
	triggers{
		pollSCM("* * * * *")
	}
	stages{
		stage('Build'){
			steps{
				echo "Build"
				sh 'dotnet build HistoryService.csproj'
			}
		}
		stage("Prepare services"){
			steps{
				echo "Prepare services"
				// Start a Docker container here
                script {
                    docker.image('history-service:tag').run('-d -p 9002:80') // Modify the image name and port as needed
                }
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