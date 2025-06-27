pipeline {
    agent any

    environment {
        IMAGE_NAME = 'video_proj-api'
        CONTAINER_NAME = 'video_proj_container'
        HOST_PORT = '5000'
        CONTAINER_PORT = '8080'
    }

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/shahid8872/video_proj.git'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o published'
            }
        }

        stage('Docker Build') {
            steps {
                bat "docker build -t %IMAGE_NAME% ."
            }
        }

        stage('Docker Cleanup (Old Container)') {
            steps {
                bat """
                docker stop %CONTAINER_NAME% || echo Not running
                docker rm %CONTAINER_NAME% || echo Already removed
                """
            }
        }

        stage('Docker Run') {
            steps {
                bat "docker run -d --name %CONTAINER_NAME% -p %HOST_PORT%:%CONTAINER_PORT% %IMAGE_NAME%"
            }
        }

        stage('Deploy') {
            steps {
                echo '🚀 App is running in Docker. Access via http://localhost:5000/swagger'
            }
        }
    }

    post {
        failure {
            echo '❌ Build or deployment failed. Check logs above.'
        }
    }
}
