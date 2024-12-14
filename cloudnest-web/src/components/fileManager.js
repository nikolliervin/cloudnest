import React from 'react';
import { FileManagerComponent, Inject, NavigationPane, DetailsView, Toolbar } from '@syncfusion/ej2-react-filemanager';
import { registerLicense } from '@syncfusion/ej2-base';

var licenseKey = process.env.REACT_APP_SYNCFUSION_LICENSE;
registerLicense(licenseKey);

const FileManager = () => {
  return (
    <div
      id="file"
      style={{
        flex: 1,
        width: '100vw',
        height: '100%',
        overflow: 'hidden', 
      }}
      className="e-control e-filemanager e-lib e-fe-cb-select e-keyboard"
    >
      <FileManagerComponent
        id="file-manager"
        view="LargeIcons"
        ajaxSettings={{
          downloadUrl:
            'https://ej2-aspcore-service.azurewebsites.net/api/FileManager/Download',
          getImageUrl:
            'https://ej2-aspcore-service.azurewebsites.net/api/FileManager/GetImage',
          uploadUrl:
            'https://ej2-aspcore-service.azurewebsites.net/api/FileManager/Upload',
          url:
            'https://ej2-aspcore-service.azurewebsites.net/api/FileManager/FileOperations',
        }}
        created={() => console.log('File Manager has been created successfully')}
        style={{
          flex: 1,
          width: '88%',
          height: '100%',
          overflow: 'hidden',
          marginLeft:'9px'
        }}
        navigationPaneSettings={{
          maxWidth: '850px',
          minWidth: '140px',
          visible: true,
        }}
      >
        <Inject services={[NavigationPane, DetailsView, Toolbar]} />
      </FileManagerComponent>
    </div>
  );
};

export default FileManager;
