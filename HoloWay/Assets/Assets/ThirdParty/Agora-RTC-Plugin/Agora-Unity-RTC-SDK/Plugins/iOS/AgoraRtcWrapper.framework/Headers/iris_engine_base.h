#pragma once

#include "iris_module.h"

class IApiEngineBase {
 public:
  virtual ~IApiEngineBase() {}
  virtual int CallIrisApi(ApiParam *apiParam) = 0;

  // IrisRtcRawData
  virtual void Attach(IrisVideoFrameBufferManagerPtr manager_ptr) {}

  virtual void Detach(IrisVideoFrameBufferManagerPtr manager_ptr) {}
};

IRIS_API IApiEngineBase *IRIS_CALL createIrisRtcEngine(void *engine);

IRIS_API void IRIS_CALL destroyIrisRtcEngine(IApiEngineBase *engine);