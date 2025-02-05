﻿// SPDX-License-Identifier: MIT
// Copyright (c) Vector Informatik GmbH. All rights reserved.

using System.Runtime.InteropServices;

namespace SilKit.Services.PubSub;

public class DataPublisher : IDataPublisher
{
  private readonly Participant _participant;

  private IntPtr _dataPublisherPtr;

  internal IntPtr DataPublisherPtr
  {
    get
    {
      return _dataPublisherPtr;
    }
    private set
    {
      _dataPublisherPtr = value;
    }
  }

  internal DataPublisher(Participant participant, string controllerName, PubSubSpec dataSpec, byte history)
  {
    _participant = participant;
    var silKitDataSpec = dataSpec.ToSilKitDataSpec();

    Helpers.ProcessReturnCode(
      (Helpers.SilKit_ReturnCodes)SilKit_DataPublisher_Create(
        out _dataPublisherPtr,
        _participant.ParticipantPtr,
        controllerName,
        silKitDataSpec,
        history),
      System.Reflection.MethodBase.GetCurrentMethod()?.MethodHandle);
  }

  /*
      SilKit_DataPublisher_Create(
          SilKit_DataPublisher** outPublisher,
          SilKit_Participant* participant,
          const char* controllerName,
          SilKit_DataSpec* dataSpec, 
          uint8_t history);
  */
  [DllImport("SilKit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
  private static extern int SilKit_DataPublisher_Create(
    [Out] out IntPtr outPublisher,
    [In] IntPtr participant,
    [In] [MarshalAs(UnmanagedType.LPStr)] string controllerName,
    [In] IntPtr dataSpec,
    [In] byte history);

  public void Publish(byte[] data)
  {
    var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
    var dataPtr = handle.AddrOfPinnedObject();
    var byteVector = new ByteVector
    {
      data = dataPtr,
      size = (IntPtr)data.Length
    };
    Helpers.ProcessReturnCode(
      (Helpers.SilKit_ReturnCodes)SilKit_DataPublisher_Publish(DataPublisherPtr, byteVector),
      System.Reflection.MethodBase.GetCurrentMethod()?.MethodHandle);
    handle.Free();
  }

  /*
      SilKit_DataPublisher_Publish(
          SilKit_DataPublisher* self, 
          const SilKit_ByteVector* data);
  */
  [DllImport("SilKit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
  private static extern int SilKit_DataPublisher_Publish(IntPtr self, ByteVector data);
}
