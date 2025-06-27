pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        PROJECT_PATH = 'video_proj/video_proj.csproj'
        OUTPUT_DIR = 'published'
        IMAGE_NAME = 'video_proj-api'
        CONTAINER_NAME = 'video_proj_container'
    }

    stages {
        stage('Checkout') {
            steps {
                git url: 'https://github.com/shahid8872/video_proj.git'
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
                bat 'dotnet test --no-build --verbosity normal'
            }
        }

        stage('Publish') {
            steps {
                bat "dotnet publish %PROJECT_PATH% -c Release -o %OUTPUT_DIR%"
            }
        }

        stage('Docker Build') {
            steps {
                bat "docker build -t %IMAGE_NAME% ./video_proj"
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
                bat "docker run -d -p 8080:80 --name %CONTAINER_NAME% %IMAGE_NAME%"
            }
        }

        stage('Deploy') {
            steps {
                echo "Application deployed in Docker container: %CONTAINER_NAME%"
            }
        }
    }

    post {
        failure {
            echo '❌ Build or deployment failed. Check logs above.'
        }
        success {
            echo '✅ Build and deployment completed successfully.'
        }
    }
}
