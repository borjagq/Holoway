#!/usr/bin/env bash

set -e

docker run \
  -e BUILD_NAME \
  -e UNITY_LICENSE \
  -e BUILD_TARGET \
  -e UNITY_USERNAME \
  -e UNITY_PASSWORD \
  -w /project/ \
  -v $UNITY_DIR: App/HoloWay \
  $IMAGE_NAME \
  /bin/bash -c "App/HoloWay/ci/before_script.sh && App/HoloWay/ci/build.sh"
