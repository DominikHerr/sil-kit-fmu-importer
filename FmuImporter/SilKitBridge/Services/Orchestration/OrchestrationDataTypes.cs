﻿// SPDX-License-Identifier: MIT
// Copyright (c) Vector Informatik GmbH. All rights reserved.

namespace SilKit.Services.Orchestration;

// LifecycleService
public delegate void CommunicationReadyHandler();

public delegate void StopHandler();

public delegate void ShutdownHandler();

// TimeSyncService
public delegate void SimulationStepHandler(UInt64 nowInNs, UInt64 durationInNs);
