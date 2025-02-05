﻿// SPDX-License-Identifier: MIT
// Copyright (c) Vector Informatik GmbH. All rights reserved.

namespace FmuImporter.Config;

public class ConfiguredVariablePublic
{
  public string? VariableName { get; set; }
  public string? TopicName { get; set; }
  public Transformation? Transformation { get; set; }
}
