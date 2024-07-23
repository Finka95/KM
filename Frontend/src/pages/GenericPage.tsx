import { CssBaseline, Box, Container, Divider } from "@mui/material"
import { FC, PropsWithChildren, ReactElement } from "react"
import Header from "../components/header/header"
import Footer from "../components/footer/footer"

interface Props {
  title: string;
  icons?: ReactElement[]

}

const GenericPage: FC<PropsWithChildren<Props>> = ({title, icons, children}) => {
  return (
    <>
      <CssBaseline />
      <Box 
        sx={{ 
          backgroundColor: "rgb(233, 216, 255)",
          minHeight: '100vh',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center' 
        }}>
        <Container 
          maxWidth="sm"
          sx={{ 
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column'
          }}>
          <Header title={title} icons={icons} />
          <Box 
            sx={{ 
              flex: 1,
              backgroundColor: "#FFFFFF",
              padding: "0 16px"
            }}>
            {children}
          </Box>
          <Divider />
          <Footer />
        </Container>
      </Box>
    </>
  )
}

export default GenericPage