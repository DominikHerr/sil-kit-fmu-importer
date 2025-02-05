﻿// SPDX-License-Identifier: MIT
// Copyright (c) Vector Informatik GmbH. All rights reserved.

using Fmi.Exceptions;

namespace Fmi.Binding;

internal static class Helpers
{
  public static void ProcessReturnCode(Fmi2Statuses statusCode, RuntimeMethodHandle? methodHandle)
  {
    ProcessReturnCode((int)statusCode, statusCode.ToString(), methodHandle);
  }

  public static void ProcessReturnCode(Fmi3Statuses statusCode, RuntimeMethodHandle? methodHandle)
  {
    ProcessReturnCode((int)statusCode, statusCode.ToString(), methodHandle);
  }

  public static void ProcessReturnCode(int resultCode, string returnCodeName, RuntimeMethodHandle? methodHandle)
  {
    var result = Common.Helpers.ProcessReturnCode(resultCode, returnCodeName, methodHandle);
    if (result.Item1)
    {
      return;
    }

    try
    {
      throw new NativeCallException(result.Item2?.ToString());
    }
    catch (Exception e)
    {
      Fmi.Helpers.Log(Fmi.Helpers.LogSeverity.Error, e.Message);
      throw;
    }
  }
}
