﻿// SPDX-License-Identifier: MIT
// Copyright (c) Vector Informatik GmbH. All rights reserved.

using Fmi.Binding.Helper;

namespace Fmi.Binding;

public interface IFmiBindingCommon : IDisposable
{
  public void GetValue(uint[] valueRefs, out ReturnVariable result, VariableTypes type);

  public void SetValue(uint valueRef, byte[] data);
  public void SetValue(uint valueRef, byte[] data, int[] binSizes);

  public void DoStep(
    double currentCommunicationPoint,
    double communicationStepSize,
    out double lastSuccessfulTime);

  public void Terminate();

  public FmiVersions GetFmiVersion();
}
