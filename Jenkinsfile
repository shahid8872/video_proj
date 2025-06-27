pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    }

    triggers {
        githubPush() // Trigger pipeline on GitHub push
    }

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/shahid8872/video_proj.git'
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore video_proj/video_proj.csproj'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build video_proj/video_proj.csproj --no-restore'
            }
        }

        stage('Test') {
            steps {
                echo 'No tests found – skipping for now.'
                // Optional: Add test project later
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish video_proj/video_proj.csproj -c Release -o out'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploy step goes here. Example: SCP, Docker, Azure CLI, etc.'
                // Example placeholder:
                // sh 'scp -r out/* user@yourserver:/var/www/video_proj'
            }
        }
    }

    post {
        success {
            echo '✅ Build and deployment succeeded!'
        }
        failure {
            echo '❌ Build or deployment failed. Check logs above.'
        }
    }
}
