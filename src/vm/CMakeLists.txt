SET(CSHARP_SOURCES AssemblyInfo.cs Eval.cs  Exception.cs TypeResolver.cs Types/BasicTypes.cs Types/Scope.cs )
SET(ASSEMBLY_NAME "Shoggoth.VM.dll")

# ADD_CUSTOM_COMMAND (
#     OUTPUT ${Shoggoth_BIN_DIR}/${ASSEMLY_NAME}
#  )

ADD_CUSTOM_TARGET( shoggoth_vm COMMAND ${CMAKE_CSHARP_COMPILER} "/target:library" "/out:${Shoggoth_BIN_DIR}/${ASSEMBLY_NAME}" ${CSHARP_SOURCES} WORKING_DIRECTORY ${Shoggoth_SOURCE_DIR}/src/vm
  COMMENT "Create ${ASSEMBLY_NAME}" )

ADD_DEPENDENCIES(all shoggoth_vm)

#  SET(DEP ${DEP} ${GDCM_EXECUTABLE_DIR}/${example}.exe)
