import React, { useState, useEffect } from 'react';

// Syncfusion FileManager
import {
  FileManagerComponent,
  Inject,
  NavigationPane,
  DetailsView,
  Toolbar,
} from '@syncfusion/ej2-react-filemanager';
import { registerLicense } from '@syncfusion/ej2-base';


// Register the Syncfusion license
registerLicense(process.env.syncfusionLicense);

const FileManager = () => {
  // State for managing the theme
  const [theme, setTheme] = useState('dark'); // Default to dark theme

  // Effect to load the appropriate theme
  useEffect(() => {
    // Remove any existing Syncfusion theme styles
    const existingLink = document.getElementById('syncfusion-theme');
    if (existingLink) {
      existingLink.remove();
    }

    // Create a new link element for the selected theme
    const link = document.createElement('link');
    link.id = 'syncfusion-theme';
    link.rel = 'stylesheet';
    link.href =
      theme === 'dark'
        ? 'https://cdn.syncfusion.com/ej2/material3-dark.css'
        : 'https://cdn.syncfusion.com/ej2/material3.css';

    document.head.appendChild(link);
  }, [theme]); // Run effect whenever theme changes

  return (
    <div
      style={{
        display: 'flex',
        flexDirection: 'column',
        height: '100vh',
        margin: 0,
        padding: 0,
        overflow: 'hidden',
      }}
    >
      {/* Toggle Button */}
      <button
        onClick={() => setTheme(theme === 'dark' ? 'light' : 'dark')}
        style={{
          margin: '10px',
          padding: '10px',
          cursor: 'pointer',
          position: 'absolute',
          zIndex: 1000,
        }}
      >
        Toggle to {theme === 'dark' ? 'Light' : 'Dark'} Theme
      </button>

      {/* File Manager Component */}
      <div
        style={{
          flex: 1,
          width: 'calc(100% - 250px)', // Adjust width if dashboard menu is 250px
          height: '100%',
          overflow: 'hidden',
          marginLeft: '250px',
          marginTop: '10px' // Adjust based on your sidebar's width
        }}
      >
        <FileManagerComponent
          id="file"
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
            width: '100%',
            height: '100%',
            overflow: 'hidden'
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
    </div>
  );
};

export default FileManager;
