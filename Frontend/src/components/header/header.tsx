import { AppBar, Box, Container, IconButton, Toolbar, Typography } from "@mui/material";
import { FC, ReactElement } from "react";
interface HeaderProps {
  title: string,
  icons?: ReactElement[]
}

const Header: FC<HeaderProps> = ({ title, icons }) => {
  return ( 
    <AppBar
      position="sticky"
      sx={{
        backgroundColor: "#FFFFFF",
        top: "0",
      }}>
      <Container>
      <Toolbar
        disableGutters
        sx={{
          justifyContent: 'space-between'
        }}>
          <Typography
            variant="h6"
            component="div"
            sx={{
              flexGrow: 1,
              color: "#000000"
            }}>
            {title}
          </Typography>
          <Box>
            {icons?.map((icon, index) => {
              return <IconButton key={index} >{icon}</IconButton>
            })}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  )
}

export default Header;